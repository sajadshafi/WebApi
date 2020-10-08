using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using School.ApplicationLogic.I_Managers;
using School.Models;
using School.Models.Custom;

namespace School.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly IAuthenticationManager manager;

        public AuthenticationController(IAuthenticationManager manager) {
            this.manager = manager;
        }

        
        [HttpPost,Route("/register")]
        public async Task<IActionResult> Register(UserProfile user) {
            var response=await manager.Register(user);
            return Ok(response);
        }

        [HttpPost, Route("/login")]
        public async Task<IActionResult> Login(LoginModel model) {
            var response = await manager.Login(model);
            return Ok(response);
        }
    }
}
