
SET XACT_ABORT ON
BEGIN TRANSACTION

INSERT INTO [dbo].[fw_system_option] ([fws_key], [fws_option], [fws_option_desc], [fws_option_type], [fws_value], [fws_category], [fws_add_user], [fws_add_date])
VALUES ('9CCACD19-AC77-41C9-BC66-B6A0552F2532', N'reCAPTCHA-PublicKey', N'The public key or site key for the reCAPTCHA account.', N'TextBox', N'', N'CMS', N'AddedInnovation', GETDATE())

INSERT INTO [dbo].[fw_system_option] ([fws_key], [fws_option], [fws_option_desc], [fws_option_type], [fws_value], [fws_category], [fws_encrypt_flag], [fws_add_user], [fws_add_date])
VALUES ('FAD6A163-7177-4B14-BFDA-C753F057ABB5', N'reCAPTCHA-PrivateKey', N'The private key or secret for the reCAPTCHA account.', N'TextBox', N'', N'CMS', 1, N'AddedInnovation', GETDATE())

INSERT INTO [dbo].[fw_system_option] ([fws_key], [fws_option], [fws_option_desc], [fws_option_type], [fws_value], [fws_category], [fws_add_user], [fws_add_date])
VALUES ('595B109F-550A-4952-86A9-CCCEF4A780B7', N'reCAPTCHA-Enabled', N'Turns reCAPTCHA on or off for the entire site.', N'CheckBox', N'true', N'CMS', N'AddedInnovation', GETDATE())

INSERT INTO [dbo].[fw_system_option] ([fws_key], [fws_option], [fws_option_desc], [fws_option_type], [fws_value], [fws_category], [fws_add_user], [fws_add_date])
VALUES ('C73008A9-869C-4462-8E11-747664233A23', N'reCAPTCHA-ClientScriptFilePath', N'The URL to the reCAPTCHA loading script. [NOTE: If the value begins with a "~" or a "/" then the application path will be prepended to it automatically in code.]', N'TextBox', N'/Scripts/AddedInnovation/netFORUM.reCAPTCHA.js', N'CMS', N'AddedInnovation', GETDATE())

COMMIT TRANSACTION

EXECUTE [md_cache_reset]