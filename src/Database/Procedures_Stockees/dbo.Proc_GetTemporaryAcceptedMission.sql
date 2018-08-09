CREATE PROCEDURE [dbo].[Proc_GetTemporaryAcceptedMission]

	@idAvaibility INT

AS

	SELECT [tbl_avaibility].[avaibility_AcceptedMission]
	FROM [tbl_avaibility]
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0