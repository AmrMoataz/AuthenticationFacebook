using BussinessLayer.Configurations;
using BussinessLayer.DTOs;
using BussinessLayer.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BussinessLayer.Services.Classes
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private const string TokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";

        protected readonly FacebookAuthConfig _facebookAuthConfig;
        protected readonly IHttpClientFactory _httpClientFactory;
        public FacebookAuthService(IOptions<FacebookAuthConfig> facebookAuthConfig, IHttpClientFactory httpClientFactory)
        {
            _facebookAuthConfig = facebookAuthConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        public DTOFacebookUserInfoResult GetUserInfo(string accessToken)
        {
            var formattedUrl = string.Format(UserInfoUrl, accessToken);

            var result = _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.Result.EnsureSuccessStatusCode();
            var responseAsString = result.Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DTOFacebookUserInfoResult>(responseAsString.Result);
        }

        public DTOFacebookTokenValidationResult ValidateAccessToken(string accessToken)
        {
           
            var formattedUrl = string.Format(TokenValidationUrl, accessToken, _facebookAuthConfig.AppId, _facebookAuthConfig.AppSecret);

            var result = _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            //result.Result.EnsureSuccessStatusCode();
            var responseAsString = result.Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DTOFacebookTokenValidationResult>(responseAsString.Result);
        }
    }
}
