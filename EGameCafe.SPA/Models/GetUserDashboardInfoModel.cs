using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class GetUserDashboardInfoModel
    {
        public GetUserDashboardInfoModel()
        {
            GameList = new List<GetUserDashboardGameDto>();
            ActivityList = new List<GetUserDashboardActivityDto>();
            SystemInfo = new UserSystemInfoModel();
            PersonalInfo = new GetUserDashboardPersonalDto();
            Friends = new List<GetUserDashboardFriendsDto>();
        }

        public IList<GetUserDashboardGameDto> GameList { get; set; }
        public IList<GetUserDashboardActivityDto> ActivityList { get; set; }
        public IList<GetUserDashboardFriendsDto> Friends { get; set; }
        public UserSystemInfoModel SystemInfo { get; set; }
        public GetUserDashboardPersonalDto PersonalInfo { get; set; }
        public string UserId { get; set; }
    }

    public class GetUserDashboardGameDto
    {
        public string GameId { get; set; }
        public string GameName { get; set; }
    }

    public class GetUserDashboardActivityDto
    {
        public string ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public string ActivityText { get; set; }
        public DateTime Created { get; set; }
    }

    public class GetUserDashboardPersonalDto
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        public DateTime Created { get; set; }

    }

    public class GetUserDashboardFriendsDto
    {
        public string FriendId { get; set; }
        public string FriendUsername { get; set; }
    }

}
