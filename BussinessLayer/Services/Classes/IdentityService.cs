using BussinessLayer.Configurations;
using BussinessLayer.DTOs;
using BussinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BussinessLayer.Services.Classes
{
    public class IdentityService : IIdentityService
    {
        protected readonly UserManager<IdentityUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly IFacebookAuthService _facebookAuthService;
        protected readonly JwtConfig _jwtConfig;

        public IdentityService(UserManager<IdentityUser> userManager, IFacebookAuthService facebookAuthService, RoleManager<IdentityRole> roleManager, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _facebookAuthService = facebookAuthService;
            _roleManager = roleManager;
            _jwtConfig = jwtConfig.Value;
        }

        public DTOAuthenticationResult LoginWithFacebook(string accessToken)
        {
            var validatedToken = _facebookAuthService.ValidateAccessToken(accessToken);
            if(validatedToken.Data == null |  validatedToken.Data.IsValid == false)
            {
                return new DTOAuthenticationResult
                {
                    Errors = new[] { "Invalid Facebook token" }
                };
            }

            var userInfo = _facebookAuthService.GetUserInfo(accessToken);
            var user = _userManager.FindByEmailAsync(userInfo.Email);
            if (user.Result == null)
            {
                var identityUser = new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = userInfo.Email,
                    UserName = userInfo.Email

                };

                var createdResult = _userManager.CreateAsync(identityUser);
                if(!createdResult.Result.Succeeded)
                {
                    return new DTOAuthenticationResult
                    {
                        Errors = new[] { "Something Went Wrong" }
                    };
                }

                return GenerateAuthenticationResultForUser(identityUser);
            }

            return GenerateAuthenticationResultForUser(user.Result);

        }

        private DTOAuthenticationResult GenerateAuthenticationResultForUser(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secert);

            var claims = new List<Claim>
            {
                new Claim (JwtRegisteredClaimNames.Sub, user.Email),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim ("id", user.Id)
            };

            var userClaims = _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims.Result);

            var userRoles = _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles.Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = _roleManager.GetClaimsAsync(role.Result);
                foreach (var roleClaim in roleClaims.Result)
                {
                    if (claims.Contains(roleClaim)) continue;
                    claims.Add(roleClaim);
                }
            }

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtConfig.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(TokenDescriptor);
            return new DTOAuthenticationResult { Token = tokenHandler.WriteToken(token), Success = true };
        }
    }
}
