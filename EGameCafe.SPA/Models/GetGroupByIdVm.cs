using EGameCafe.SPA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class GetGroupByIdVm
    {
        public GetGroupByIdVm()
        {
            GroupMembers = new List<GetGroupByIdGroupMemberDto>();
            GroupInfo = new GetGroupByIdInfoDto();
        }
        public IList<GetGroupByIdGroupMemberDto> GroupMembers { get; set; }
        public GetGroupByIdInfoDto GroupInfo { get; set; }
    }

    public class GetGroupByIdInfoDto
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public GroupType GroupType { get; set; }
        public string GameName { get; set; }
        public string SharingLink { get; set; }
    
    }

    public class GetGroupByIdGroupMemberDto
    {
        public bool IsBlock { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }

    }
}
