CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyStreet]

	@idEmploymentAgency INT

AS

	SELECT [tbl_employmentAgency].[employmentAgency_Street]
	FROM [tbl_employmentAgency]
	WHERE [tbl_employmentAgency].[idEmploymentAgency]=@idEmploymentAgency;

RETURN 0