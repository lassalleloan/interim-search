CREATE PROCEDURE [dbo].[Proc_GetTemporaryBirthDate]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_BirthDate]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0