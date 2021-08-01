using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.DTOs
{
    public class DTOFacebookUserInfoResult
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("picture")]
        public DTOFacebookPicture FacebookPicture { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class DTOFacebookPicture
    {
        [JsonProperty("data")]
        public DTOFacebookPictureData FacebookPictureData { get; set; }
    }

    public class DTOFacebookPictureData
    {
        [JsonProperty("height")]
        public long Height { get; set; }
        [JsonProperty("is_silhouette")]
        public bool isSilhouette { get; set; }
        [JsonProperty("url")]
        public Uri Url { get; set; }
        [JsonProperty("width")]
        public long Width { get; set; }
    }
}
