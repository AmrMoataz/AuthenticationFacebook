using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.DTOs
{
    public class DTOFacebookTokenValidationResult
    {
        [JsonProperty("data")]
        public DTOFacebookTokenValidationData Data { get; set; }
    }

    public class DTOFacebookTokenValidationData
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("Application")]
        public string Application { get; set; }
        [JsonProperty("data_access_expires_at")]
        public long DataAccessExpiresAt { get; set; }
        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
        [JsonProperty("scopes")]
        public string[] Scopes { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
