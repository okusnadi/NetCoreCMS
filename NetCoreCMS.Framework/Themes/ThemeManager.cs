﻿using Microsoft.Extensions.Logging;
using NetCoreCMS.Framework.Core;
using NetCoreCMS.Framework.Core.Messages;
using NetCoreCMS.Framework.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;

namespace NetCoreCMS.Framework.Themes
{
    public class ThemeManager
    {
        public static readonly string ThemeInfoFileName = "Theme.json";
        ThemeManager _themeManager;
        ILoggerFactory _loggerFactory;
        ILogger _logger;
 
        public ThemeManager(ILoggerFactory factory)
        {
            ILoggerFactory _loggerFactory = factory;
            _logger = _loggerFactory.CreateLogger<ThemeManager>();
        }

        public List<Theme> ScanThemeDirectory(string path)
        {
            List<Theme> themes = new List<Theme>();
            var directoryInfo = new DirectoryInfo(path);
            foreach (var themeDir in directoryInfo.EnumerateDirectories())
            {
                try
                {
                    var configFileLocation = Path.Combine(themeDir.FullName, ThemeInfoFileName);
                    if (File.Exists(configFileLocation))
                    {
                        var themeInfoFileContent = File.ReadAllText(configFileLocation);
                        var theme = JsonConvert.DeserializeObject<Theme>(themeInfoFileContent);
                        themes.Add(theme);
                        if (theme.IsActive)
                        {
                            GlobalConfig.ActiveTheme = theme;
                        }
                    }
                    else
                    {
                        RegisterErrorMessage("Theme config file Theme.json not found");
                    }                    

                }
                catch(Exception ex)
                {
                    RegisterErrorMessage(ex.Message);
                    _logger.LogError(ex.ToString());
                }                
            }
            return themes;
        }

        public bool ActivateTheme(string themeName)
        {
            try
            {
                var infoFileLocation = Path.Combine(GlobalConfig.ContentRootPath, NccInfo.ThemeFolder, themeName,ThemeInfoFileName);
                if (File.Exists(infoFileLocation))
                {
                    var themeInfoFileContent = File.ReadAllText(infoFileLocation);
                    var theme = JsonConvert.DeserializeObject<Theme>(themeInfoFileContent);
                    
                    if (theme.IsActive == false)
                    {
                        if (InactivateTheme(GlobalConfig.ActiveTheme.ThemeName))
                        {
                            theme.IsActive = true;
                            GlobalConfig.ActiveTheme = theme;
                            var themeJson = JsonConvert.SerializeObject(theme,Formatting.Indented);
                            File.WriteAllText(infoFileLocation, themeJson);
                        }
                        else
                        {
                            RegisterErrorMessage("Previous theme inactivation failed.");
                        }
                        
                    }

                    return true;
                }
                else
                {
                    RegisterErrorMessage("Theme config file Theme.json not found");
                }

            }
            catch (Exception ex)
            {
                RegisterErrorMessage(ex.Message);
                _logger.LogError(ex.ToString());
            }
            return false;
        }
        
        public bool InactivateTheme(string themeName)
        {
            try
            {
                var infoFileLocation = Path.Combine(GlobalConfig.ContentRootPath, NccInfo.ThemeFolder, themeName, ThemeInfoFileName);
                if (File.Exists(infoFileLocation))
                {
                    var themeInfoFileContent = File.ReadAllText(infoFileLocation);
                    var theme = JsonConvert.DeserializeObject<Theme>(themeInfoFileContent);

                    if (theme.IsActive == true)
                    {
                        theme.IsActive = false;
                        GlobalConfig.ActiveTheme = theme;
                        var themeJson = JsonConvert.SerializeObject(theme, Formatting.Indented);
                        File.WriteAllText(infoFileLocation, themeJson);
                    }

                    return true;
                }
                else
                {
                    RegisterErrorMessage("Theme config file Theme.json not found");
                }

            }
            catch (Exception ex)
            {   
                _logger.LogError(ex.ToString());
            }
            return false;
        }

        public static void RegisterThemes(IMvcBuilder mvcBuilder, IServiceCollection services, IDirectoryContents themes )
        {
            var themeDlls = new List<Assembly>();
            foreach (var themeFolder in themes.Where(x => x.IsDirectory))
            {
                try
                {
                    var binFolder = new DirectoryInfo(Path.Combine(themeFolder.PhysicalPath, "bin"));
                    if (!binFolder.Exists)
                    {
                        continue;
                    }

                    foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                    {
                        Assembly assembly;
                        try
                        {
                            assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                        }
                        catch (FileLoadException ex)
                        {
                            continue;
                        }
                        catch (BadImageFormatException ex)
                        {
                            continue;
                        }

                        if (assembly.FullName.Contains(themeFolder.Name))
                        {
                            themeDlls.Add(assembly);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not load module from " + themeFolder);
                }
            } 
            
            mvcBuilder.AddRazorOptions(o =>
            {
                foreach (var module in themeDlls)
                {
                    o.AdditionalCompilationReferences.Add(MetadataReference.CreateFromFile(module.Location));
                }
            });
        }

        private void RegisterErrorMessage(string message)
        {
            GlobalMessageRegistry.RegisterMessage(
                new GlobalMessage()
                {
                    Registrater = typeof(ThemeManager).Name,
                    Text = message,
                    Type = GlobalMessage.MessageType.Error,
                    For = GlobalMessage.MessageFor.Admin
                },
                new TimeSpan(0, 1, 0)
            );
        }
    }
}
