﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreCMS.Framework.Core.Mvc.Controllers;
using NetCoreCMS.Framework.Core.Services;
using NetCoreCMS.Framework.Setup;
using NetCoreCMS.Framework.Utility;
using System.Linq;

namespace NetCoreCMS.Core.Modules.Cms.Controllers
{
    [AllowAnonymous]
    public class CmsHomeController : NccController
    {
        private NccPageService _pageService;
        
        public CmsHomeController(NccPageService pageService, ILoggerFactory factory)
        {
            _pageService = pageService;
            _logger = factory.CreateLogger<CmsHomeController>();
        }

        public ActionResult Index()
        {
            if (SetupHelper.IsDbCreateComplete && SetupHelper.IsAdminCreateComplete)
            {
                ViewBag.Content = "";
                ViewBag.Layout = "";
                var page = _pageService.GetBySlugs("Home");
                if (page != null)
                {
                    ViewBag.Content = page.Content;
                    if (GlobalConfig.ActiveTheme.Layouts.Where(l => l.Name.Contains(page.Layout)).Count() > 0)
                    {
                        ViewBag.Layout = page.Layout;
                    }

                }
                return View();
            }
            return Redirect("/SetupHome/Index");
        }
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult ResourceNotFound()
        {   
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
         
    }
}
