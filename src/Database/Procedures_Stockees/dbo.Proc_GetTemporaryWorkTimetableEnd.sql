CREATE PROCEDURE [dbo].[Proc_GetTemporaryWorkTimetableEnd]

	@idAvaibility INT

AS

	SELECT [tbl_workTimetable].[workTimetable_End]
	FROM [tbl_avaibility]
	INNER JOIN [tbl_workTimetable] ON [tbl_avaibility].[idWorkTimetable]=[tbl_workTimetable].[idWorkTimetable]
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0