CREATE PROCEDURE [dbo].[Proc_GetTemporaryDate]

	@idAvaibility INT

AS

	SELECT [tbl_avaibility].[avaibility_Date]
	FROM [tbl_avaibility]
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0