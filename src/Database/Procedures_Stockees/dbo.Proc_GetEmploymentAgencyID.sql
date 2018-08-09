CREATE PROCEDURE [dbo].[Proc_GetEmploymentAgencyID]

	@idTemporary INT

AS

	SELECT [tbl_employmentAgency].[idEmploymentAgency]
	FROM [tbl_temporary]
	INNER JOIN [tbl_employmentAgency] ON [tbl_temporary].[idEmploymentAgency]=[tbl_employmentAgency].[idEmploymentAgency]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0