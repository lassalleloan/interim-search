CREATE PROCEDURE [dbo].[Proc_GetTemporaryWorkReport]

	@idTemporary INT

AS

	SELECT [avaibility_Date], [tbl_workTimetable].[workTimetable_Start], [tbl_workTimetable].[workTimetable_End], [tbl_service].[service_Name]
	FROM [tbl_avaibility]
	INNER JOIN [tbl_workTimetable] ON [tbl_avaibility].[idWorkTimetable]=[tbl_workTimetable].[idWorkTimetable]
	INNER JOIN [tbl_service] ON [tbl_avaibility].[idService]=[tbl_service].[idService]
	WHERE [tbl_avaibility].[idTemporary]=@idTemporary
	AND [tbl_avaibility].[avaibility_AcceptedMission]=1;

RETURN 0