using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Account;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.DataAccess.Entities;
using Microsoft.AspNetCore.Http;

namespace XamarinBlogEducation.Business.Services
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<string> SignIn(LoginAccountViewModel model)
        {
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new ApplicationException("User is not found.");
            }

            var isEmailConfirm = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirm)
            {
                throw new ApplicationException("Email is not confirmed");
            }

            var token = await GetToken(user);
            return token;
        }
        public async Task<EditAccountViewModel> FindUser(LoginAccountViewModel model)
        {
   
            var user = await _userManager.FindByEmailAsync(model.Email);
            var editUser = new EditAccountViewModel();
            editUser.Email = user.Email;
            editUser.FirstName = user.FirstName;
            editUser.LastName = user.LastName;
            editUser.UserImage = user.UserImage;
            if (user == null)
            {
                throw new ApplicationException("User is not found.");
            }
            return editUser;
        }
        public async Task<bool> CreateUser(RegisterAccountViewModel model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                throw new ApplicationException($"Email {model.Email} is already taken.");
            }

            var newUser = new ApplicationUser();
            newUser.Email = model.Email;
            newUser.UserName = model.Email.Substring(0, model.Email.IndexOf("@"));
            newUser.EmailConfirmed = true;
            newUser.FirstName = model.FirstName;
            newUser.LastName = model.LastName;
            newUser.UserImage = model.UserImage;
            var identityResult = await _userManager.CreateAsync(newUser, model.Password);
            if (!identityResult.Succeeded)
            {
                throw new ApplicationException("");
            }
           return identityResult.Succeeded;
        }

        public async Task RemoveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);
        }

        public async Task UpdateUserProfile(EditAccountViewModel model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (_userManager.FindByEmailAsync(model.Email)== null)
            {
            user.Email = model.Email;
            }
            user.UserName= model.Email.Substring(0, model.Email.IndexOf("@"));
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserImage = model.UserImage;
            await _userManager.UpdateAsync(user);

        }
        public async Task ChangeUserPassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = model.Token;
            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                //ex
            }
            else
            {
                if (model.Password != model.ConfirmPassword)
                {
                    //ex
                }
                else
                {
                    await _userManager.ResetPasswordAsync(user, token, model.Password);
                    
                }
            }
        }
        private async Task<string> GetToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userFullName = $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}";
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, userFullName),
                new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty)
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList());
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration.GetValue<string>("Jwt:LIFETIME")));

            var token = new JwtSecurityToken(

                issuer: _configuration.GetValue<string>("Jwt:ISSUER"),
                audience: _configuration.GetValue<string>("Jwt:AUDIENCE"),
                notBefore: DateTime.Now,
                claims: claims,
                expires: expires,
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
