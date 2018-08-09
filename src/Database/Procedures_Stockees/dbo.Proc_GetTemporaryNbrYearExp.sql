CREATE PROCEDURE [dbo].[Proc_GetTemporaryNbrYearExp]

	@idTemporary INT

AS

	SELECT YEAR([tbl_temporary].[temporary_StartDateExperience]) AS 'temporary_YearStartDateExperience'
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0