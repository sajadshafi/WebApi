using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using School.ApplicationLogic.I_Managers;
using School.Configurations;
using School.Helpers;
using School.Models;
using School.Models.Custom;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace School.ApplicationLogic.Managers {
    public class AuthenticationManager : IAuthenticationManager {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationSetting applicationSetting;

        public AuthenticationManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSetting> applicationSetting) {
            _userManager = userManager;
            this.signInManager = signInManager;
            this.applicationSetting = applicationSetting.Value;
        }

        public async Task<ResponseMessage<LoginResponse>> Login(LoginModel model) {
            var result = await this.signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, true);
            if (result.Succeeded) {
                var user = await _userManager.FindByNameAsync(model.Username);
                var descriptor = new SecurityTokenDescriptor();

                descriptor.Subject = new ClaimsIdentity(new Claim[] { new Claim("UserId", user.Id.ToString()) });
                descriptor.Expires = DateTime.UtcNow.AddDays(10);
                descriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationSetting.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature);


                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(descriptor);
                var token = tokenHandler.WriteToken(securityToken);

                LoginResponse loginResponse = new LoginResponse {
                    Name = user.Name,
                    Username = user.UserName,
                    token = token
                };
                return new ResponseMessage<LoginResponse> {
                    Success = true,
                    Data = loginResponse,
                    Message = Constants.UserLoginSuccessfully
                };
            }
            return new ResponseMessage<LoginResponse> {
                Success = true,
                Data = null,
                Message = Constants.InvalidCredentials
            };
        }
        public async Task<ResponseMessage<IdentityResult>> Register(UserProfile user) {
            ApplicationUser _user = new ApplicationUser() {
                Email = user.Email,
                UserName = user.Username,
                Name = user.Name
            };
            var result = await _userManager.CreateAsync(_user, user.Password);
            return new ResponseMessage<IdentityResult>() {
                Success = true,
                Data = result,
                Message = result.Succeeded ? Constants.UserRegisteredSuccessFully : Constants.UserRegisterationFailed
            };
        }

    }
}
