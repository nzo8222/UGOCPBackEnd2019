using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Models
{
    public class SessionModel
    {
        public string UserId { get; set; }
        public int ExpiresIn { get; set; }
        public string JwtToken { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        
    }
}
