# reCAPTCHA-netFORUM
Easily add Google reCAPTCHA to your netFORUM forms

# Installation
## Create reCAPTCHA Account
1. Navigate to [Google reCAPTCHA page](https://www.google.com/recaptcha/admin)
1. Sign up for an account
1. Follow the instructions. Note: for testing, you can specify "localhost" for your URL. **Important: You must specify your production domain urls in the Domains list for your reCAPTCHA site for the reCAPTCHA to work on your non-localhost url.**

## Install netFORUM Components
1. Download or Clone Repo
1. Build Added.Recaptcha.netFORUM Project
1. Copy Added.Recaptcha.dll and Added.Recaptcha.netFORUM.dll to /eWeb/bin/ directory
1. Copy contents of Added Recaptcha.netFORUM project /Scripts/AddedInnovation/* to your eWeb Site Root/Scripts/AddedInnovation
1. Execute reCAPTCHA_MD.sql SQL Script in Added.Recaptcha.netFORUM project /SQL Scripts/ folder
1. Update reCAPTCHA-PublicKey, reCAPTCHA-PrivateKey, and reCAPTCHA-Enabled system options from iWeb with the values specified by your Google reCAPTCHA Account. Note: Site Key and Public Key are synonymous. **Important: the reCAPTCHA-PrivateKey is encrypted so it must be updated through iWeb and not a SQL Script. Also, this value should not be shared.**

## Configure netFORUM Components
### Add Form Extension
1. Add Form Extension to form
1. Control Class: Div
1. Control ID: addedRecaptcha (name it whatever you want)
1. Object Assembly: Added.Recaptcha.netFORUM
1. Oject Typename: Added.Recaptcha.netFORUM.RecaptchaExtensions
1. See V2 reCAPTCHA and Invisible reCAPTCHA below for Methods
1. Add form extension control to form (Form Designer) in the location it should display
#### V2 reCAPTCHA
1. Load Method: OnLoad_RenderRecaptcha
1. Load Parameters: Page:Page;Control:Control
1. Before Save Method: BeforeSave_ValidateRecaptcha
1. Before Save Parameters: Page:Page;Control:Control

#### Invisible reCAPTCHA
1. Load Method: OnLoad_RenderRecaptcha
1. Load Parameters: Page:Page;Control:Control
1. Before Save Method: BeforeSave_ValidateRecaptcha
1. Before Save Parameters: Page:Page;Control:Control
1. Navigate to wizard buttons which should trigger invisible reCAPTCHA
1. Add "invoke-recaptcha" css class to all wizard buttons which should trigger reCAPTCHA

#### Back to Top Button
To deal with the Back to top button and the Invisible reCAPTCHA overlap, you can use the following code to move the reCAPTCHA out of the way of the back to top button:

***Note:*** there are curly braces that are netFORUM-escaped with [| or |] to be able to be dropped into an eWeb page detail easily.

```
<style type="text/css">
.g-recaptcha [|
    margin-left: 100px;
    margin-top: 40px;
|]
</style>
<script type="text/javascript">
var handler = function () [|
        var offset = 20; // offset used by FormFunctions for the Back To Top link.
        var duration = 400; // duration used by FormFunctions for the Back To Top link.
        var enabled = false;

        function _tryAttach() [|
            if($('.grecaptcha-badge').length == 0) [|
                window.setTimeout(_tryAttach, duration);
            |]
            else [|
                window.setTimeout(function () [|
                    _handler();
                    $(window).scroll(_handler);
                |], duration);
            |]
        |];

        var _handler = function () [|
            if (enabled === false && $(window).scrollTop() > offset) [|
                enabled = true;
                $('.grecaptcha-badge').animate([| bottom: '80px' |], duration);
            |] 
            else if (enabled === true && $(window).scrollTop() <= offset) [|
                enabled = false;
                $('.grecaptcha-badge').animate([| bottom: '14px' |], duration);
            |]
        |];

        _tryAttach();
|]

if (typeof (Sys) !== 'undefined' && typeof (Sys.WebForms) !== 'undefined' && typeof (Sys.WebForms.PageRequestManager) !== 'undefined') [|
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(handler);
|]
else [|
    $(handler);
|]
</script>
```
