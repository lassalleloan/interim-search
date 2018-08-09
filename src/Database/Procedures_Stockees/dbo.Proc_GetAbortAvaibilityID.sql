CREATE PROCEDURE [dbo].[Proc_GetAbortAvaibilityID]

	@idAvaibility INT

AS

	SELECT [tbl_avaibility].[idAvaibility]
	FROM [tbl_avaibility]
	WHERE [tbl_avaibility].[avaibility_Date]=(SELECT [tbl_avaibility].[avaibility_Date] FROM [tbl_avaibility] WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility)
	AND [tbl_avaibility].[idWorkTimetable]=(SELECT [tbl_avaibility].[idWorkTimetable] FROM [tbl_avaibility] WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility)
	AND [tbl_avaibility].[idService]=(SELECT [tbl_avaibility].[idService] FROM [tbl_avaibility] WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility)
	AND [tbl_avaibility].[avaibility_AcceptedMission] IS NULL;

RETURN 0