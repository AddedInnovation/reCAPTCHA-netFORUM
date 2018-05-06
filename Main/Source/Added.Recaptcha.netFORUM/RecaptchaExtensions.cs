using Avectra.netForum.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Added.Recaptcha.netFORUM
{
    public class RecaptchaExtensions
    {
        protected const string LoadingScriptKey = "Added.netFORUM.reCAPTCHA.js";

        protected const string RecaptchaScriptKey = "Added.reCAPTCHA.js";

        public const string RecaptchaPublicKeySystemOption = "reCAPTCHA-PublicKey";

        public const string RecaptchaPrivateKeySystemOption = "reCAPTCHA-PrivateKey";

        public const string RecaptchaEnabledSystemOption = "reCAPTCHA-Enabled";

        public static RecaptchaConfiguration CreateRecaptchaConfigurationFromSystemOptions()
        {
            var config = RecaptchaConfiguration.LoadFromConfigFile();

            var sitekey = Config.GetSystemOption(RecaptchaPublicKeySystemOption);
            var secretkey = Config.GetSystemOption(RecaptchaPrivateKeySystemOption);
            var enabled = config.Enabled;

            if (!String.IsNullOrWhiteSpace(sitekey))
            {
                config.PublicKey = sitekey;
            }

            if (!String.IsNullOrWhiteSpace(secretkey))
            {
                config.PrivateKey = secretkey;
            }

            config.Enabled = (!Boolean.TryParse(Config.GetSystemOption(RecaptchaEnabledSystemOption), out enabled) || enabled);

            return config;
        }

        public void AddLoadingScript(Page page)
        {
            if (!page.ClientScript.IsClientScriptIncludeRegistered(LoadingScriptKey))
            {
                page.ClientScript.RegisterClientScriptInclude(LoadingScriptKey, String.Concat(Config.Context.Request.ApplicationPath.Trim(), "/Scripts/AddedInnovation/netFORUM.reCAPTCHA.js"));
            }
        }

        public void AddRecaptchaScript(Page page)
        {
            AddRecaptchaScript(page, false);
        }

        public void AddRecaptchaScript(Page page, bool isExplicit)
        {
            AddRecaptchaScript(page, isExplicit, null);
        }

        public void AddRecaptchaScript(Page page, bool isExplicit, string callback)
        {
            AddLoadingScript(page);

            if (!page.ClientScript.IsStartupScriptRegistered(RecaptchaScriptKey))
            {
                var url = String.Concat(RecaptchaConstants.JavascriptApiSourceUrl, "?render=", (isExplicit ? "explicit" : "onload"));

                if (!String.IsNullOrWhiteSpace(callback))
                {
                    url = String.Concat(url, "&onload=", callback);
                }

                page.ClientScript.RegisterStartupScript(GetType(), RecaptchaScriptKey, $"<script type=\"text/javascript\" src=\"{url}\" async defer></script>", false);
            }
        }

        public void OnLoad_RenderRecaptcha(Page page, Control cntrl)
        {
            var config = CreateRecaptchaConfigurationFromSystemOptions();

            InjectRecaptcha(page, cntrl, new VisibleRecaptchaOptions(config));
        }

        public void OnLoad_RenderInvisibleRecaptcha(Page page, Control cntrl)
        {
            var config = CreateRecaptchaConfigurationFromSystemOptions();

            InjectRecaptcha(page, cntrl, new InvisibleRecaptchaOptions(config));
        }

        public void BeforeSave_ValidateRecaptcha(Page page, Control control)
        {
            var err = ErrorFactory.CreateInstance();

            try
            {
                var config = CreateRecaptchaConfigurationFromSystemOptions();

                if (config.Enabled == false)
                {
                    return;
                }

                var response = Config.Context.Request.Form[RecaptchaConstants.ResponseFormFieldName];

                var result = RecaptchaValidator.Validate(config.PrivateKey, response);

                if (result == null)
                {
                    err.Message = RecaptchaConstants.UnableToValidateMessage;
                }
                else if (result.IsSuccess == false)
                {
                    err.Message = RecaptchaConstants.ValidationFailureMessage;
                }

                err.UserMessage = err.Message;
            }
            catch (Exception ex)
            {
                err = ErrorFactory.CreateInstance(ex);
            }
            finally
            {
                if (!String.IsNullOrWhiteSpace(err.Message))
                {
                    err.Number = (int)ErrorClass.ErrorNumber.ValidationFailed;
                    err.Level = ErrorClass.ErrorLevel.Error;
                }

                if (UtilityFunctions.ER(err))
                {
                    Config.LastError = err;
                }
                else
                {
                    Config.ClearLastError();
                }
            }
        }

        public void InjectRecaptcha(Page page, Control target, RecaptchaOptions options)
        {
            if (options.Enabled == false)
            {
                return;
            }

            var control = target as HtmlControl;

            if (control == null)
            {
                throw new ArgumentException("Control argument cannot be null.", nameof(target));
            }

            if (control.HasControls())
            {
                foreach (HtmlControl cntrl in control.Controls)
                {
                    if (cntrl.HasControls() == false &&
                        cntrl.TagName.Equals(HtmlTextWriterTag.Div.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        control = cntrl;
                        break;
                    }
                }
            }

            control.Attributes.Add("class", RecaptchaConstants.DefaultElementCssClass);

            var json = options.SerializeToJson();
            var items = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);

            foreach (var item in items)
            {
                control.Attributes.Add($"data-{item.Key}", item.Value);
            }

            AddLoadingScript(page);

            var script =
                @"(function ($) {
                    var container = '" + control.ClientID + @"';
                    var payload = '" + json + @"';

                    netFORUM.onPageLoad(function () {
		                netFORUM.reCAPTCHA.renderRecaptcha(container, payload);
                        $('[data-invoke-recaptcha], .invoke-recaptcha').each(function (index, elem) {
                            netFORUM.reCAPTCHA.wireRecaptchaChallenge(elem);
                        });
	                });
                })(typeof(jQuery) !== 'undefined' ? jQuery : null);";

            page.ClientScript.RegisterStartupScript(GetType(), GetType().Namespace, script, true);

            AddRecaptchaScript(page, true);
        }
    }
}