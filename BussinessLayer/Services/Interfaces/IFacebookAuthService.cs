using BussinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services.Interfaces
{
    public interface IFacebookAuthService
    {
        DTOFacebookTokenValidationResult ValidateAccessToken(string accessToken);
        DTOFacebookUserInfoResult GetUserInfo(string accessToken);
    }
}
