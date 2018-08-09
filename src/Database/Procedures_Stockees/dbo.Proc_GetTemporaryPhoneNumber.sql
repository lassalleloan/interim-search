CREATE PROCEDURE [dbo].[Proc_GetTemporaryPhoneNumber]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_PhoneNumber]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0