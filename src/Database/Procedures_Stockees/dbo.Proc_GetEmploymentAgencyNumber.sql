CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyNumber]

	@idEmploymentAgency INT

AS

	SELECT [tbl_employmentAgency].[employmentAgency_Number]
	FROM [tbl_employmentAgency]
	WHERE [tbl_employmentAgency].[idEmploymentAgency]=@idEmploymentAgency;

RETURN 0