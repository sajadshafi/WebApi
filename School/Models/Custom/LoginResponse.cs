using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Models.Custom {
    public class LoginResponse {
        public string Username { get; set; }
        public string Name { get; set; }
        public string token { get; set; }
    }
}
