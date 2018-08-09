CREATE PROCEDURE [dbo].[Proc_GetAvaibilityTemporaryID]

	@idAvaibility INT

AS

	SELECT [tbl_avaibility].[idTemporary]
	FROM [tbl_avaibility]
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0