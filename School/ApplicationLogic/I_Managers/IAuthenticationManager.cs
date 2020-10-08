using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Configurations;
using School.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.ApplicationLogic.I_Managers {
    public interface IAuthenticationManager {
        Task<ResponseMessage<IdentityResult>> Register(UserProfile user);
        Task<ResponseMessage<LoginResponse>> Login(LoginModel model);
    }
}
