CREATE PROCEDURE [dbo].[Proc_GetService]

AS

	SELECT [tbl_service].[service_Name]
	FROM [tbl_service]
	ORDER BY [tbl_service].[service_Name];

RETURN 0