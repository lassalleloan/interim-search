CREATE PROCEDURE [dbo].[Proc_GetTemporaryNumber]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_Number]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0