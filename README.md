# reCAPTCHA-netFORUM
Easily add Google reCAPTCHA to your netFORUM forms

# Installation
## Create reCAPTCHA Account
1. Navigate to [Google reCAPTCHA page](https://www.google.com/recaptcha/admin)
1. Sign up for an account
1. Follow the instructions. Note: for testing, you can specify "localhost" for your URL

## Install netFORUM Components
1. Download or Clone Repo
1. Build Added.Recaptcha.netFORUM Project
1. Copy Added.Recaptcha.dll and Add.Recaptcha.netFORUM.dll to /eWeb/bin/ directory
1. Copy contents of Added Recaptcha.netFORUM project /Scripts/AddedInnovation/* to your eWeb Site Root/Scripts/AddedInnovation
1. Execute reCAPTCHA_MD.sql SQL Script in Added.Recaptcha.netFORUM project /SQL Scripts/ folder
1. Update reCAPTCHA-PublicKey, reCAPTCHA-PrivateKey, and reCAPTCHA-Enabled system options from iWeb the values specified by your Google reCAPTCHA Account. Note: Site Key and Public Key are synonymous

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


