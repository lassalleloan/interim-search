CREATE PROCEDURE [dbo].[Proc_SetAbortMission]

	@idAvaibility INT

AS

	UPDATE [tbl_avaibility] SET [tbl_avaibility].[idService]=NULL, [tbl_avaibility].[avaibility_SendDate]=NULL,[tbl_avaibility].[avaibility_AcceptedMission]=NULL
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0