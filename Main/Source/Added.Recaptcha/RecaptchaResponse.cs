using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Added.Recaptcha
{
    /// <remarks>
    /// This is a modernized version of the solution provided by Google for ASP.NET reCAPTCHA.
    /// </remarks>
    public class RecaptchaResponse
    {
        #region Implementation

        protected const string IncorrectSolutionErrorCode = "incorrect-captcha-sol";

        protected const string ServerNotReachableErrorCode = "recaptcha-not-reachable";

        protected RecaptchaResponse(bool valid, string error)
        {
            IsValid = valid;
            ErrorMessage = error;
        }

        public bool IsValid { get; protected set; }

        public string ErrorMessage { get; protected set; }

        public static RecaptchaResponse CreateErrorResponse(string error)
        {
            return new RecaptchaResponse(false, error);
        }

        #endregion Implementation

        #region Known Responses

        static readonly RecaptchaResponse _Valid = new RecaptchaResponse(true, String.Empty);

        static readonly RecaptchaResponse _InvalidChallenge = new RecaptchaResponse(false, "Invalid reCAPTCHA request. Missing challenge value.");

        static readonly RecaptchaResponse _InvalidResponse = new RecaptchaResponse(false, "Invalid reCAPTCHA request. Missing response value.");

        static readonly RecaptchaResponse _IncorrectSolution = new RecaptchaResponse(false, "The verification words are incorrect.");

        static readonly RecaptchaResponse _ServerNotReachable = new RecaptchaResponse(false, "The reCAPTCHA server is unavailable.");

        public static RecaptchaResponse Valid => _Valid;

        public static RecaptchaResponse InvalidChallenge => _InvalidChallenge;

        public static RecaptchaResponse InvalidResponse => _InvalidResponse;

        public static RecaptchaResponse IncorrectSolution => _IncorrectSolution;

        public static RecaptchaResponse ServerNotReachable => _ServerNotReachable;

        #endregion Known Responses

        #region Base Object Members

        public override bool Equals(object other)
        {
            var response = other as RecaptchaResponse;
            return response?.IsValid == IsValid && response?.ErrorMessage == ErrorMessage;
        }

        public override int GetHashCode()
        {
            return IsValid.GetHashCode() ^ ErrorMessage.GetHashCode();
        }

        #endregion Base Object Members
    }
}