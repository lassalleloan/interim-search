CREATE PROCEDURE [dbo].[Proc_GetAvaibilityID]

	@avaibility_Date DATE,
	@idTemporary INT

AS

	SELECT [tbl_avaibility].[idAvaibility]
	FROM [tbl_avaibility]
	WHERE [tbl_avaibility].[avaibility_Date]=@avaibility_Date
	AND [tbl_avaibility].[idTemporary]=@idTemporary;

RETURN 0