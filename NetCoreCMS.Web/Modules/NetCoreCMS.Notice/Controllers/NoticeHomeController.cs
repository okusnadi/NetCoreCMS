﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreCMS.Framework.Core.Mvc.Controllers;
using NetCoreCMS.Framework.Themes;
using NetCoreCMS.Framework.Utility;
using NetCoreCMS.Notice.Models;
using NetCoreCMS.Notice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static NetCoreCMS.Notice.Models.NccNotice;

namespace NetCoreCMS.Notice.Controllers
{

    [Authorize(Roles = "SuperAdmin,Administrator,Editor")]
    [AdminMenu(Name = "Notice", Order = 100)]
    public class NoticeHomeController : NccController
    {
        private NccNoticeService _nccNoticeService;
        public NoticeHomeController(NccNoticeService nccNoticeService)
        {
            _nccNoticeService = nccNoticeService;
        }

        [AllowAnonymous]
        public ActionResult Index(int page = 0, int pageSize = 10)
        {
            var all = _nccNoticeService.LoadByStatusAndPage(NccNotice.NccNoticeStatus.Published, NccNotice.NccNoticeType.Site, page, pageSize);
            return View(all);
        }

        [AllowAnonymous]
        public ActionResult Details(long id)
        {
            var notice = _nccNoticeService.Get(id);
            if (notice == null)
                return RedirectToAction("/Home/Error");
            return View(notice);
        }

        [AdminMenuItem(Name = "New Notice", Url = "/NoticeHome/CreateEdit", Order = 1)]
        public ActionResult CreateEdit(long Id = 0)
        {
            NccNotice notice = new NccNotice();
            notice.PublishDate = DateTime.Today;
            notice.ExpireDate = DateTime.Today.AddDays(30);

            var NoticeStatus = Enum.GetValues(typeof(NccNoticeStatus)).Cast<NccNoticeStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            ViewBag.NoticeStatus = new SelectList(NoticeStatus, "Value", "Text", (int)notice.NoticeStatus);

            var NoticeType = Enum.GetValues(typeof(NccNoticeType)).Cast<NccNoticeType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            ViewBag.NoticeType = new SelectList(NoticeType, "Value", "Text", (int)notice.NoticeType);

            if (Id > 0)
            {
                notice = _nccNoticeService.Get(Id);
            }
            return View(notice);
        }

        [HttpPost]
        public ActionResult CreateEdit(NccNotice notice)
        {
            ViewBag.MessageType = "ErrorMessage";
            ViewBag.Message = "Error occoured. Please fill up all field correctly.";

            if (ModelState.IsValid)
            {
                if (notice.Id > 0)
                {
                    _nccNoticeService.Update(notice);
                    ViewBag.MessageType = "SuccessMessage";
                    ViewBag.Message = "Notice updated successfull.";
                }
                else
                {
                    _nccNoticeService.Save(notice);
                    ViewBag.MessageType = "SuccessMessage";
                    ViewBag.Message = "Notice save successfull.";
                }
                //TempData["SuccessMessage"] = "Notice save successfull.";
            }

            return View(notice);
        }

        [AdminMenuItem(Name = "Manage", Url = "/NoticeHome/Manage", Order = 2)]
        public ActionResult Manage()
        {
            var allNotices = _nccNoticeService.LoadAll().OrderByDescending(n => n.Id).ToList();
            return View(allNotices);
        }

        public ActionResult Delete(long Id)
        {
            NccNotice notice = _nccNoticeService.Get(Id);
            //page.
            return View(notice);
        }

        [HttpPost]
        public ActionResult Delete(long Id, int status)
        {
            _nccNoticeService.DeletePermanently(Id);
            ViewBag.MessageType = "SuccessMessage";
            ViewBag.Message = "Page deleted successful";
            return RedirectToAction("Manage");
        }
    }
}