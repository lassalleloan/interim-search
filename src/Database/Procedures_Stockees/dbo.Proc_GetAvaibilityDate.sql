CREATE PROCEDURE [dbo].[Proc_GetAvaibilityDate]

AS

	SELECT DISTINCT [tbl_avaibility].[avaibility_Date]
	FROM [tbl_avaibility]
	WHERE [tbl_avaibility].[avaibility_Date] >= CONVERT(date, GETDATE())
	AND [tbl_avaibility].[idService] IS NOT NULL
	AND [tbl_avaibility].[avaibility_SendDate] IS NOT NULL
	AND ([tbl_avaibility].[avaibility_AcceptedMission]=1
	OR [tbl_avaibility].[avaibility_AcceptedMission]=0
	OR [tbl_avaibility].[avaibility_AcceptedMission] IS NULL);

RETURN 0