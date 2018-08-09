CREATE PROCEDURE [dbo].[Proc_GetTemporaryCity]

	@idTemporary INT

AS

	SELECT [tbl_city].[city_Name]
	FROM [tbl_city]
	INNER JOIN [tbl_temporary] ON [tbl_city].[idCity]=[tbl_temporary].[idCity]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0