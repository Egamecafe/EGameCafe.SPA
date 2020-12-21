using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class GroupMember
    {
        public string GroupMemberId { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }
        //public Group Group { get; set; }
        public bool IsBlock { get; set; }
    }
}
