using BussinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services.Interfaces
{
    public interface IIdentityService
    {
        DTOAuthenticationResult LoginWithFacebook(string accessToken);
    }
}
