CREATE PROCEDURE [dbo].[Proc_GetProfession]

AS

	SELECT [tbl_profession].[profession_Name]
	FROM [tbl_profession]
	ORDER BY [tbl_profession].[profession_Name];

RETURN 0