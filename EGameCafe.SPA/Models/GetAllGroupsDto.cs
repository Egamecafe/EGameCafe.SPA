﻿using EGameCafe.SPA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class GetAllGroupsDto
    {
        public string GamingGroupGroupId { get; set; }
        public string GroupName { get; set; }
        public GroupType GroupType { get; set; }
    }
}
