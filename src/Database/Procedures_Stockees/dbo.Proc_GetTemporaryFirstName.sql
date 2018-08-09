CREATE PROCEDURE [dbo].[Proc_GetTemporaryFirstName]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_FirstName]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0