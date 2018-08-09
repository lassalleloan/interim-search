CREATE PROCEDURE [dbo].[Proc_SetStateMission]

	@idAvaibility INT,
	@avaibility_AcceptedMission INT

AS

	UPDATE [tbl_avaibility] SET [tbl_avaibility].[avaibility_AcceptedMission]=@avaibility_AcceptedMission
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0