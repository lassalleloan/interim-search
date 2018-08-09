CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyCity]

	@idEmploymentAgency INT

AS

	SELECT [tbl_city].[city_Name]
	FROM [tbl_city]
	INNER JOIN [tbl_employmentAgency] ON [tbl_city].[idCity]=[tbl_employmentAgency].[idCity]
	WHERE [tbl_employmentAgency].[idEmploymentAgency]=@idEmploymentAgency;

RETURN 0