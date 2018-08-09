CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyPostCode]

	@idEmploymentAgency INT

AS

	SELECT [tbl_employmentAgency].[employmentAgency_PostCode]
	FROM [tbl_employmentAgency]
	WHERE [tbl_employmentAgency].[idEmploymentAgency]=@idEmploymentAgency;

RETURN 0