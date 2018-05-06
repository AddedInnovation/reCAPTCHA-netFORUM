using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Added.Recaptcha
{
    /// <summary>
    /// Represents the base set of options for configuring a reCAPTCHA widget.
    /// </summary>
    public abstract class RecaptchaOptions
    {
        public RecaptchaOptions()
            : this(RecaptchaConfiguration.LoadFromConfigFile())
        { }

        public RecaptchaOptions(RecaptchaConfiguration config)
        {
            Configuration = config;
            Enabled = config.Enabled;
        }

        protected RecaptchaConfiguration Configuration { get; set; }

        /// <summary>
        /// The public key for the registered reCAPTCHA account.
        /// </summary>
        [JsonProperty("sitekey")]
        public string SiteKey => Configuration?.PublicKey;

        /// <summary>
        /// The private key for the registered reCAPTCHA account.
        /// </summary>
        [JsonIgnore]
        public string SecretKey => Configuration?.PrivateKey;

        /// <summary>
        /// If set to false, the reCAPTCHA widget will be disabled.
        /// </summary>
        [JsonIgnore]
        public bool Enabled { get; set; }

        /// <summary>
        /// The tabindex of the challenge. If other elements in your page use tabindex, it should be set to make user navigation easier.
        /// </summary>
        [JsonProperty("tabindex")]
        public int? TabIndex { get; set; }

        /// <summary>
        /// The name of your callback function, executed when the user submits a successful response. The g-recaptcha-response token is passed to your callback.
        /// </summary>
        [JsonProperty("callback")]
        public string Callback { get; set; }

        /// <summary>
        /// The name of your callback function, executed when the reCAPTCHA response expires and the user needs to re-verify.
        /// </summary>
        [JsonProperty("expired-callback")]
        public string ExpiredCallback { get; set; }

        /// <summary>
        /// The name of your callback function, executed when reCAPTCHA encounters an error (usually network connectivity) and cannot continue until connectivity is restored. If you specify a function here, you are responsible for informing the user that they should retry.
        /// </summary>
        [JsonProperty("error-callback")]
        public string ErrorCallback { get; set; }

        /// <summary>
        /// Size of the reCAPTCHA control.
        /// </summary>
        [JsonProperty("size")]
        public abstract string Size { get; }

        public virtual IDictionary<string, string> ToDictionary()
        {
            return JsonConvert.DeserializeObject<IDictionary<string, string>>(SerializeToJson());
        }

        public virtual string SerializeToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }

    /// <summary>
    /// Represents all possible options for configuring a traditional, visible checkbox reCAPTCHA.
    /// </summary>
    public class VisibleRecaptchaOptions : RecaptchaOptions
    {
        public VisibleRecaptchaOptions()
            : base()
        { }

        public VisibleRecaptchaOptions(RecaptchaConfiguration config)
            : base(config)
        { }

        /// <summary>
        /// The color theme of the reCAPTCHA widget that is rendered. By default, this will be set to 'light' unless the <see cref="UseDarkTheme"/> property is set to true.
        /// </summary>
        [JsonProperty("theme")]
        public string Theme => (UseDarkTheme ? Recaptcha.Theme.Dark : Recaptcha.Theme.Light).ToString().ToLower();

        /// <summary>
        /// Directs the reCAPTCHA that it should instead render the widget in the darker color scheme.
        /// </summary>
        [JsonIgnore]
        public bool UseDarkTheme { get; set; }

        /// <summary>
        /// Size of the reCAPTCHA control. By default, this will be set to 'normal' unless the <see cref="IsCompact"/> property is set to true.
        /// </summary>
        [JsonProperty("size")]
        public override string Size => (IsCompact ? Recaptcha.Size.Compact : Recaptcha.Size.Normal).ToString().ToLower();

        [JsonIgnore]
        public bool IsCompact { get; set; }
    }

    /// <summary>
    /// Represents all possible options for configuring an invisible reCAPTCHA.
    /// </summary>
    public class InvisibleRecaptchaOptions : RecaptchaOptions
    {
        public InvisibleRecaptchaOptions()
            : base()
        { }

        public InvisibleRecaptchaOptions(RecaptchaConfiguration config)
            : base(config)
        { }

        /// <summary>
        /// The location of the reCAPTCHA badge on the screen. By default, this will be set to 'bottomright' unless the <see cref="DefaultScreenSideForBadgePosition"/> property is set to 'Left' or the <see cref="HasCustomBadgePosition"/> property is set to true.
        /// </summary>
        [JsonProperty("badge")]
        public string Badge => (HasCustomBadgePosition ? BadgeLocation.Inline : (DefaultScreenSideForBadgePosition == ScreenSide.Left ? BadgeLocation.BottomLeft : BadgeLocation.BottomRight)).ToString().ToLower();

        /// <summary>
        /// Size of the reCAPTCHA control. This will always be set to 'invisisble' for this type of reCAPTCHA.
        /// </summary>
        [JsonProperty("size")]
        public override string Size => Recaptcha.Size.Invisible.ToString().ToLower();

        /// <summary>
        /// For plugin owners to not interfere with existing reCAPTCHA installations on a page. If true, this reCAPTCHA instance will be part of a separate ID space. Defaults to false.
        /// </summary>
        [JsonProperty("isolated")]
        public bool Isolated { get; set; }

        /// <summary>
        /// By default, the badge will appear on the bottom right unless specified otherwise. If the <see cref="HasCustomBadgePosition"/> property is set to true, this property will be ignored.
        /// </summary>
        [JsonIgnore]
        public ScreenSide DefaultScreenSideForBadgePosition { get; set; }

        /// <summary>
        /// If true, the badge can be positioned with CSS by the user. If set to true, this property will override the value of the <see cref="DefaultScreenSideForBadgePosition"/> property.
        /// </summary>
        [JsonIgnore]
        public bool HasCustomBadgePosition { get; set; }
    }
}
