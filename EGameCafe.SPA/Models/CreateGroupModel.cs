using EGameCafe.SPA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class CreateGroupModel
    {
        public string GroupName { get; set; }
        public GroupType GroupType { get; set; }
        public string GameId { get; set; }
        public string CreatorId { get; set; }
    }
}
