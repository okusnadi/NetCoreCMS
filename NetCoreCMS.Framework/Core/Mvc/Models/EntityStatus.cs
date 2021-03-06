﻿namespace NetCoreCMS.Framework.Core.Mvc.Models
{
    public class EntityStatus
    {
        public static int Deleted { get { return -8; } }
        public static int Archived { get { return -4; } }
        public static int Inactive { get { return -2; } }
        public static int New { get { return 0; } }
        public static int Active { get { return 2; } }
        public static int Modified { get { return 4; } }
    }
}
