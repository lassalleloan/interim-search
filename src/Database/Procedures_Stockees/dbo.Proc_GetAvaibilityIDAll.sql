CREATE PROCEDURE [dbo].[Proc_GetAvaibilityIDAll]

	@avaibility_Date DATE,
	@workTimetable_Start TIME(0),	
	@workTimetable_End TIME(0)

AS	

	SELECT [tbl_avaibility].[idAvaibility]
	FROM [tbl_avaibility]
	INNER JOIN [tbl_workTimetable] ON [tbl_avaibility].[idWorkTimetable]=[tbl_workTimetable].[idWorkTimetable]
	WHERE [tbl_avaibility].[avaibility_Date]=@avaibility_Date
	AND [tbl_workTimetable].[workTimetable_Start]=@workTimetable_Start
	AND [tbl_workTimetable].[workTimetable_End]=@workTimetable_End
	AND [tbl_avaibility].[idService] IS NOT NULL
	AND [tbl_avaibility].[avaibility_SendDate] IS NOT NULL;

RETURN 0