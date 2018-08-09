CREATE PROCEDURE [dbo].[Proc_GetTemporaryName]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_Name]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0