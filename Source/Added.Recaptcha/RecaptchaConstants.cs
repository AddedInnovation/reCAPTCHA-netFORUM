using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Added.Recaptcha
{
    public class RecaptchaConstants
    {
        #region Miscellaneous

        public const string AppSettingPrefix = "recaptcha";

        #endregion Miscellaneous

        #region URLs & Endpoints

        public const string VerificationApiEndpoint = "https://www.google.com/recaptcha/api/siteverify";

        public const string JavascriptApiSourceUrl = "https://www.google.com/recaptcha/api.js";

        #endregion URLs & Endpoints

        #region Form Values

        public const string ResponseFormFieldName = "g-recaptcha-response";

        public const string DefaultElementId = "recaptcha";

        public const string DefaultElementCssClass = "g-recaptcha";

        #endregion Form Values

        #region POST Parameters

        public const string PrivateKeyParameterName = "secret";

        public const string ResponseParameterName = "response";

        public const string RemoteIPParameterName = "remoteip";

        #endregion POST Parameters

        #region Javascript API Parameters

        public const string OnLoadParameterName = "onload";

        public const string RenderParameterName = "render";

        public const string LanguageCodeParameterName = "hl";

        #endregion Javascript API Parameters

        #region API Response Fields

        public const string SuccessResponseFieldName = "success";

        public const string ChallengeTimestampResponseFieldName = "challenge_ts";

        public const string HostnameResponseFieldName = "hostname";

        public const string ErrorCodesResponseFieldName = "error-codes";

        #endregion API Response Fields

        #region API Error Codes

        public const string MissingPrivateKeyErrorCode = "missing-input-secret";

        public const string InvalidPrivateKeyErrorCode = "invalid-input-secret";

        public const string MissingResponseErrorCode = "missing-input-response";

        public const string InvalidResponseErrorCode = "invalid-input-response";

        public const string BadRequestErrorCode = "bad-request";

        public const string IncorrectSolutionErrorCode = "incorrect-captcha-sol";

        public const string ServerNotReachableErrorCode = "recaptcha-not-reachable";

        #endregion API Error Codes

        #region Default Error Messages

        public const string UnableToValidateMessage = "Unable to validate reCAPTCHA- please try again or contact site administrator.";

        public const string ValidationFailureMessage = "Invalid reCAPTCHA response- please try again.";

        #endregion Default Error Messages
    }
}
