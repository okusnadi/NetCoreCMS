﻿using NetCoreCMS.Framework.Core.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreCMS.Framework.Core.Models
{
    public class NccMenuItem : IBaseModel<long>
    {
        public NccMenuItem()
        {
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
            CreateBy = ModifyBy = BaseModel.GetCurrentUserId();
            Status = EntityStatus.New;
            VersionNumber = 1;
            SubActions = new List<NccMenuItem>();
            Childrens = new List<NccMenuItem>();            
        }

        [Key]
        public long Id { get; set; }
        public int VersionNumber { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public long CreateBy { get; set; }
        public long ModifyBy { get; set; }
        public int Status { get; set; }
        public NccMenuItem Parent { get; set; }
        public string Module { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string Data { get; set; }
        public string Target { get; set; }
        public int Position { get; set; }
        public string MenuIconCls { get; set; }
        public int MenuOrder { get; set; }
        public ActionType MenuActionType { get; set; }
        public MenuItemFor MenuFor { get; set; }
        public List<NccMenuItem> SubActions { get; set; }
        public List<NccMenuItem> Childrens { get; set; }


        public enum ActionType
        {
            Url,
            Page,
            BlogPost,
            BlogCategory,
            Module,
            Tag,
        }

        public enum MenuItemFor
        {
            Site,
            Admin
        }

        public class MenuItemTarget
        {
            public static string Blank { get { return "_blank"; } }
            public static string Parent { get { return "_parent"; } }
            public static string Self { get { return "_self"; } }
            public static string Top { get { return "_top"; } }
        }
    }    
}
