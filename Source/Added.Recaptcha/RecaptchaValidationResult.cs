using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Added.Recaptcha
{
    public class RecaptchaValidationResult
    {
        [JsonProperty(RecaptchaConstants.SuccessResponseFieldName)]
        public bool IsSuccess { get; set; }

        [JsonProperty(RecaptchaConstants.ChallengeTimestampResponseFieldName)]
        public DateTime Challenge { get; set; }

        [JsonProperty(RecaptchaConstants.HostnameResponseFieldName)]
        public string Hostname { get; set; }

        [JsonProperty(RecaptchaConstants.ErrorCodesResponseFieldName)]
        public string[] Errors { get; set; }
    }
}
