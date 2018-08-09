CREATE PROCEDURE [dbo].[Proc_GetCountTemporaryProfession]

AS

	SELECT [tbl_profession].[profession_Name], COUNT([tbl_temporary].[idTemporary]) AS 'count_Temporary'
	FROM [tbl_temporary]
	INNER JOIN [tbl_profession] ON [tbl_temporary].[idProfession]=[tbl_profession].[idProfession]
	GROUP BY [tbl_profession].[profession_Name];

RETURN 0