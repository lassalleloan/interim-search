CREATE PROCEDURE [dbo].[Proc_GetTemporaryPostCode]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_PostCode]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0