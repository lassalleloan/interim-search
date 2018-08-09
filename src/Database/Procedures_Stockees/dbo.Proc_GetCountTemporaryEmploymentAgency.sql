CREATE PROCEDURE [dbo].[Proc_GetCountTemporaryEmploymentAgency]

AS

	SELECT [tbl_employmentAgency].[employmentAgency_Name], COUNT([tbl_temporary].[idTemporary]) AS 'count_Temporary'
	FROM [tbl_temporary]
	INNER JOIN [tbl_employmentAgency] ON [tbl_temporary].[idEmploymentAgency]=[tbl_employmentAgency].[idEmploymentAgency]
	GROUP BY [tbl_employmentAgency].[employmentAgency_Name];	

RETURN 0