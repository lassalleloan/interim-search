CREATE PROCEDURE [dbo].[Proc_GetTemporaryCertified]

	@idTemporary INT

AS

	SELECT [tbl_temporary].[temporary_Certified]
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0