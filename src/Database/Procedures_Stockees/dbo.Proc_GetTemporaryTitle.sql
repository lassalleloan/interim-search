CREATE PROCEDURE [dbo].[Proc_GetTemporaryTitle]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_Title]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0