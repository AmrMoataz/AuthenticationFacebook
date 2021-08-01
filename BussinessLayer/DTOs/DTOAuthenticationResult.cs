using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.DTOs
{
    public class DTOAuthenticationResult
    {
        public string[] Errors { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
    }
}
