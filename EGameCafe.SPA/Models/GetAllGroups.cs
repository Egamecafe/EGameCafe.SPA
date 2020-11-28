using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class GetAllGroups
    {
        public GetAllGroups()
        {
            List = new List<GetAllGroupsDto>();
        }

        public IList<GetAllGroupsDto> List { get; set; }
        public int TotalGroups { get; set; }
    }
}
