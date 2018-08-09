CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyRegion]

	@idEmploymentAgency INT

AS

	SELECT [tbl_region].[region_Name]
	FROM [tbl_employmentAgency]
	INNER JOIN [tbl_city] ON [tbl_employmentAgency].[idCity]=[tbl_city].[idCity]
	INNER JOIN [tbl_region] ON [tbl_city].[idRegion]=[tbl_region].[idRegion]
	WHERE [tbl_employmentAgency].[idEmploymentAgency]=@idEmploymentAgency;

RETURN 0