CREATE PROCEDURE [dbo].[Proc_GetWorkTimetable]

AS

	SELECT DISTINCT [tbl_workTimetable].[workTimetable_Start], [tbl_workTimetable].[workTimetable_End]
	FROM [tbl_workTimetable]
	ORDER BY [tbl_workTimetable].[workTimetable_Start];

RETURN 0