﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using NetCoreCMS.Framework.Modules;
using NetCoreCMS.Framework.Core.Models;
using NetCoreCMS.Framework.Core.Services;
using NetCoreCMS.Framework.Core.Repository;
using NetCoreCMS.Framework.Themes;
using NetCoreCMS.Framework.Modules.Widgets;

namespace NetCoreCMS.Core.Modules.Cms
{
    public class CmsModule : IModule
    {
        List<IWidget> _widgets;
        public CmsModule()
        {
            
        }

        public string ModuleId { get; set; }
        public string ModuleTitle { get; set; }
        public string ModuleName { get; set; }
        public bool AntiForgery { get; set; }
        public string Author { get; set; }
        public string Website { get; set; }
        public string Version { get; set; }
        public string NetCoreCMSVersion { get; set; }
        public string Description { get; set; }
        public List<string> Dependencies { get; set; }
        public string Category { get; set; }
        public Assembly Assembly { get; set; }
        public string SortName { get; set; }
        public string Path { get; set; }
        
        public List<IWidget> Widgets { get { return _widgets; } set { _widgets = value; } }

        public bool Activate()
        {
            throw new NotImplementedException();
        }

        public bool Inactivate()
        {
            throw new NotImplementedException();
        }

        public void Init(IServiceCollection services)
        {
            services.AddTransient<NccMenuRepository>();
            services.AddTransient<NccMenuItemRepository>();
            services.AddTransient<NccMenuService>();
            services.AddTransient<NccPageRepository>();
            services.AddTransient<NccPageService>();
            services.AddTransient<ThemeManager>();
            services.AddTransient<NccWebSiteWidgetRepository>();
            services.AddTransient<NccWebSiteWidgetService>();
            services.AddTransient<NccWebSiteRepository>();
            services.AddTransient<NccWebSiteService>();
            services.AddTransient<NccThemeRepository>();
            services.AddTransient<NccThemeService>();
            services.AddTransient<NccCategoryRepository>();
            services.AddTransient<NccCategoryService>();            
        }

        public bool Install()
        {
            throw new NotImplementedException();
        }
         
        public bool Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}
