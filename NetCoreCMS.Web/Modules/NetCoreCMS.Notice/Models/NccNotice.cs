﻿using NetCoreCMS.Framework.Core.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreCMS.Notice.Models
{
    public class NccNotice : IBaseModel<long>
    {
        public NccNotice()
        {
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
            CreateBy = ModifyBy = BaseModel.GetCurrentUserId();
            Status = EntityStatus.New;
            VersionNumber = 1;
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
        
        [Required]
        public string Title { get; set; }
        [Required]        
        [MaxLength(int.MaxValue)]        
        public string Content { get; set; }        
        public NccNoticeStatus NoticeStatus { get; set; }
        public NccNoticeType NoticeType { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int NoticeOrder { get; set; }

        public enum NccNoticeStatus
        {
            Drafted,
            Published,
            UnPublished,
            Expired
        }

        public enum NccNoticeType
        {
            Site,
            AdminPanel
        }
    }
}
