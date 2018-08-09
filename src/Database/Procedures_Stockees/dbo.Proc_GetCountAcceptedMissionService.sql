CREATE PROCEDURE [dbo].[Proc_GetCountAcceptedMissionService]

AS

	SELECT [tbl_service].[service_Name], COUNT(DISTINCT [tbl_avaibility].[idAvaibility]) AS 'count_AcceptedMission'
	FROM [tbl_avaibility]
	INNER JOIN [tbl_service] ON [tbl_avaibility].[idService]=[tbl_service].[idService]
	WHERE [tbl_avaibility].[avaibility_AcceptedMission]=1
	GROUP BY [tbl_service].[service_Name];

RETURN 0