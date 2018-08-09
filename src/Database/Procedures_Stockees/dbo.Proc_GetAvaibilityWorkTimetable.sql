CREATE PROCEDURE [dbo].[Proc_GetAvaibilityWorkTimetable]

	@avaibility_Date DATE

AS

	SELECT DISTINCT [tbl_workTimetable].[workTimetable_Start], [tbl_workTimetable].[workTimetable_End]
	FROM [tbl_avaibility]
	INNER JOIN [tbl_workTimetable] ON [tbl_avaibility].[idWorkTimetable]=[tbl_workTimetable].[idWorkTimetable]
	WHERE [tbl_avaibility].[avaibility_Date]=@avaibility_Date
	AND [tbl_avaibility].[idService] IS NOT NULL
	AND [tbl_avaibility].[avaibility_SendDate] IS NOT NULL
	ORDER BY [tbl_workTimetable].[workTimetable_Start];

RETURN 0