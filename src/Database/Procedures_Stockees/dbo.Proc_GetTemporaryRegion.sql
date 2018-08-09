CREATE PROCEDURE [dbo].[Proc_GetTemporaryRegion]

	@idTemporary INT

AS

	SELECT [tbl_region].[region_Name]
	FROM [tbl_city]
	INNER JOIN [tbl_region] ON [tbl_city].[idRegion]=[tbl_region].[idRegion]
	INNER JOIN [tbl_temporary] ON [tbl_city].[idCity]=[tbl_temporary].[idCity]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0