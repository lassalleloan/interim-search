CREATE PROCEDURE [dbo].[Proc_GetTemporaryStreet]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_Street]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0