CREATE PROCEDURE [dbo].[Proc_GetTemporaryService]

	@idAvaibility INT

AS	

	SELECT [tbl_service].[service_Name]
	FROM [tbl_avaibility]
	INNER JOIN [tbl_service] ON [tbl_avaibility].[idService]=[tbl_service].[idService]
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0