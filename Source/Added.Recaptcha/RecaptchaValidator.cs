using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Added.Recaptcha
{
    /// <remarks>
    /// This is a modernized version of the solution provided by Google for ASP.NET reCAPTCHA.
    /// </remarks>
    public static class RecaptchaValidator
    {
        public static FormUrlEncodedContent BuildFormDataPayload(string secret, string response, IPAddress remoteIP = null)
        {
            var data = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(RecaptchaConstants.PrivateKeyParameterName, secret),
                    new KeyValuePair<string, string>(RecaptchaConstants.ResponseParameterName, response)
                };

            if (remoteIP != null)
            {
                data.Add(new KeyValuePair<string, string>(RecaptchaConstants.RemoteIPParameterName, remoteIP.ToString()));
            }

            return new FormUrlEncodedContent(data);
        }

        public static RecaptchaValidationResult Validate(string secret, string response, string remoteIP)
        {
            var address = default(IPAddress);

            if (!String.IsNullOrWhiteSpace(remoteIP))
            {
                if (!IPAddress.TryParse(remoteIP, out address) ||
                    (address.AddressFamily != AddressFamily.InterNetwork
                    && address.AddressFamily != AddressFamily.InterNetworkV6))
                {
                    throw new ArgumentException($"'{remoteIP}' is not a valid IPv4 or IPv6 address.", nameof(remoteIP));
                }
            }

            return Validate(secret, response, address);
        }

        public static RecaptchaValidationResult Validate(string secret, string response, IPAddress remoteIP = null)
        {
            if (secret == null)
            {
                throw new ArgumentNullException(nameof(secret), $"Argument for {nameof(secret)} must be provided to validate reCAPTCHA.");
            }

            if (response == null)
            {
                throw new ArgumentNullException(nameof(response), $"Argument for {nameof(response)} must be provided to validate reCAPTCHA.");
            }

            using (var client = new HttpClient { Timeout = new TimeSpan(0, 0, 30) })
            {
                var payload = BuildFormDataPayload(secret, response, remoteIP);
                var result = client.PostAsync(RecaptchaConstants.VerificationApiEndpoint, payload).Result;
                var content = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<RecaptchaValidationResult>(content);
            }
        }
    }
}
