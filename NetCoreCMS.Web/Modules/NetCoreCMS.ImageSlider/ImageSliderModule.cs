﻿using Microsoft.Extensions.DependencyInjection;
using NetCoreCMS.Framework.Modules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using NetCoreCMS.Framework.Core.Models;
using NetCoreCMS.Framework.Modules.Widgets;
using NetCoreCMS.ImageSlider.Widgets;
using NetCoreCMS.ImageSlider.Repository;
using NetCoreCMS.ImageSlider.Services;

namespace NetCoreCMS.Modules.ImageSlider
{
    public class ImageSliderModule : IModule
    {
        List<IWidget> _widgets;
        public ImageSliderModule()
        {
             
        }

        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string SortName { get; set; }
        public bool AntiForgery { get; set ; }
        public string Author { get ; set ; }
        public string Website { get ; set ; }
        public string Version { get ; set ; }
        public string NetCoreCMSVersion { get ; set ; }
        public string Description { get ; set ; }

        public List<string> Dependencies { get ; set ; }
        public string Category { get ; set ; }

        [NotMapped]
        public Assembly Assembly { get; set; }
        public string Path { get ; set ; }
        
        public string ModuleTitle { get ; set ; }
        [NotMapped]
        public List<IWidget> Widgets { get { return _widgets; } set { _widgets = value; } }

        public bool Activate()
        {
            return true;
        }

        public bool Inactivate()
        {
            return true;
        }

        public void Init(IServiceCollection services)
        {
            services.AddTransient<NccImageSliderRepository>();
            services.AddTransient<NccImageSliderService>();
        }

        public bool Install()
        {
            return true;
        }
          
        public bool Uninstall()
        {
            return true;
        }
    }
}
