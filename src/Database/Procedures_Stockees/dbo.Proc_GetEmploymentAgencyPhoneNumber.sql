CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyPhoneNumber]

	@idEmploymentAgency INT

AS

	SELECT [tbl_employmentAgency].[employmentAgency_PhoneNumber]
	FROM [tbl_employmentAgency]
	WHERE [tbl_employmentAgency].[idEmploymentAgency]=@idEmploymentAgency;

RETURN 0