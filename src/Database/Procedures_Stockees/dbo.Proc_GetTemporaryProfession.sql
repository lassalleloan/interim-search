CREATE PROCEDURE [dbo].[Proc_GetTemporaryProfession]

	@idTemporary INT

AS

	SELECT [tbl_profession].[profession_Name]
	FROM [tbl_profession]
	INNER JOIN [tbl_temporary] ON [tbl_profession].[idProfession]=[tbl_temporary].[idProfession]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0