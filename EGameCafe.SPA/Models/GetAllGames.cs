using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class GetAllGames
    {
        public GetAllGames()
        {
            List = new List<GameModel>();
        }

        public IList<GameModel> List { get; set; }
        public int TotalGames { get; set; }
    }
}
