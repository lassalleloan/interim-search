CREATE PROCEDURE [dbo].[Proc_GetTemporarySendDate]

	@idAvaibility INT

AS

	SELECT [tbl_avaibility].[avaibility_SendDate]
	FROM [tbl_avaibility]
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0