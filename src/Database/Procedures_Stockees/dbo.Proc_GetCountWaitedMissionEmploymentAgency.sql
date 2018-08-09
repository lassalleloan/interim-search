CREATE PROCEDURE [dbo].[Proc_GetCountWaitedMissionEmploymentAgency]

AS

	SELECT [tbl_employmentAgency].[employmentAgency_Name], COUNT(DISTINCT [tbl_avaibility].[idAvaibility]) AS 'count_WaitedMission'
	FROM [tbl_avaibility]
	INNER JOIN [tbl_temporary] ON [tbl_avaibility].[idTemporary]=[tbl_temporary].[idTemporary]
	INNER JOIN [tbl_employmentAgency] ON [tbl_temporary].[idEmploymentAgency]=[tbl_employmentAgency].[idEmploymentAgency]
	WHERE [tbl_avaibility].[avaibility_AcceptedMission] IS NULL
	GROUP BY [tbl_employmentAgency].[employmentAgency_Name];

RETURN 0