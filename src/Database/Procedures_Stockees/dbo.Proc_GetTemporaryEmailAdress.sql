CREATE PROCEDURE [dbo].[Proc_GetTemporaryEmailAdress]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_EmailAdress]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0