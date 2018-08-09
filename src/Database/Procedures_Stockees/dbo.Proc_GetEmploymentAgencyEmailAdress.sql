CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyEmailAdress]

	@idEmploymentAgency INT

AS

	SELECT [tbl_employmentAgency].[employmentAgency_EmailAdress]
	FROM [tbl_employmentAgency]
	WHERE [tbl_employmentAgency].[idEmploymentAgency]=@idEmploymentAgency;

RETURN 0