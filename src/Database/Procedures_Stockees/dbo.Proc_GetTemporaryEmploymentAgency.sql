CREATE PROCEDURE [dbo].[Proc_GetTemporaryEmploymentAgency]

	@idTemporary INT

AS

	SELECT [tbl_employmentAgency].[employmentAgency_Name]
	FROM [tbl_employmentAgency]
	INNER JOIN [tbl_temporary] ON [tbl_employmentAgency].[idEmploymentAgency]=[tbl_temporary].[idEmploymentAgency]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0