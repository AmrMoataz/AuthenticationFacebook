using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Configurations
{
    public class JwtConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Secert { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
    }
}
