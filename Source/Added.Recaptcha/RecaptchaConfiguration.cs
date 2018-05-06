using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Added.Recaptcha
{
    public class RecaptchaConfiguration
    {
        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public bool Enabled { get; set; } = true;

        public static RecaptchaConfiguration LoadFromConfigFile()
        {
            var enabled = true;

            return new RecaptchaConfiguration
            {
                PublicKey = ConfigurationManager.AppSettings[$"{RecaptchaConstants.AppSettingPrefix}:{nameof(PublicKey)}"],
                PrivateKey = ConfigurationManager.AppSettings[$"{RecaptchaConstants.AppSettingPrefix}:{nameof(PrivateKey)}"],
                Enabled = (!Boolean.TryParse(ConfigurationManager.AppSettings[$"{RecaptchaConstants.AppSettingPrefix}:{nameof(Enabled)}"], out enabled) || enabled)
            };
        }
    }
}
