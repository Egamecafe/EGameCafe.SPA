using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Models
{
    public class ChatMessageModel
    {
        public string MessageText { get; set; }
        public string UserId { get; set; }
        public bool WithImage { get; set; }
    }
}
