CREATE PROCEDURE [dbo].[Proc_GetTemporaryAge]

	@idTemporary INT

AS

	SELECT YEAR([tbl_temporary].[temporary_BirthDate]) AS 'temporary_YearBirthDate'
	FROM [tbl_temporary]
	WHERE [tbl_temporary].[idTemporary]=@idTemporary;

RETURN 0