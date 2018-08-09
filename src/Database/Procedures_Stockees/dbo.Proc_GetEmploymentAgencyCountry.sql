CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyCountry]

	@idEmploymentAgency INT

AS

	SELECT [tbl_country].[country_Name]
	FROM [tbl_employmentAgency]
	INNER JOIN [tbl_city] ON [tbl_employmentAgency].[idCity]=[tbl_city].[idCity]
	INNER JOIN [tbl_region] ON [tbl_city].[idRegion]=[tbl_region].[idRegion]
	INNER JOIN [tbl_country] ON [tbl_region].[idCountry]=[tbl_country].[idCountry]
	WHERE [tbl_employmentAgency].[idEmploymentAgency]=@idEmploymentAgency;

RETURN 0