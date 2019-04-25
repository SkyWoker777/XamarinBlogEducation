using AutoMapper;
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
using XamarinBlogEducation.Business.Exceptions;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Business.Services
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AccountService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<string> SignIn(LoginAccountRequestModel model)
        {

            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new AccountException("User is not found.");
            }

            bool isEmailConfirm = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirm)
            {
                throw new AccountException("Email is not confirmed");
            }

            string token = await GetToken(user);
            return token;
        }
        public async Task<GetInfoAccountResponseModel> FindUser(string email)
        {

            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            GetInfoAccountResponseModel editUser = _mapper.Map<GetInfoAccountResponseModel>(user);
            if (user == null)
            {
                throw new AccountException("User is not found.");
            }
            return editUser;
        }
        public async Task<bool> CreateUser(RegisterAccountRequestModel model)
        {

            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                throw new AccountException($"Email {model.Email} is already taken.");
            }

            ApplicationUser newUser = _mapper.Map<ApplicationUser>(model);
            newUser.UserName = model.Email.Substring(0, model.Email.IndexOf("@"));
            newUser.EmailConfirmed = true;
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, model.Password);
            if (!identityResult.Succeeded)
            {
                throw new AccountException("Registration faild");
            }
            return identityResult.Succeeded;
        }
        public async Task UpdateUserProfile(EditAccountRequestModel model, string id)
        {
            ApplicationUser updatedUser = _mapper.Map<ApplicationUser>(model);
            if (_userManager.FindByEmailAsync(model.Email) == null)
            {
                updatedUser.Email = model.Email;
            }
            updatedUser.Id = id;
            await _userManager.UpdateAsync(updatedUser);

        }
        public async Task ChangeUserPassword(ChangePasswordAccountRequestModel model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            string token = model.Token;
            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                throw new AccountException("Wrong password");
            }
            else
            {
                if (model.Password != model.ConfirmPassword)
                {
                    throw new AccountException("Passwords are not the same");
                }
                else
                {
                    await _userManager.ResetPasswordAsync(user, token, model.Password);
                }
            }
        }
        private async Task<string> GetToken(ApplicationUser user)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            string userFullName = $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}";
            List<Claim> claims = new List<Claim>
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
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration.GetValue<string>("Jwt:LIFETIME")));

            JwtSecurityToken token = new JwtSecurityToken(

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
