CREATE PROCEDURE [dbo].[Proc_SetSendDateAndService]

	@idAvaibility INT,
	@service_Name NVARCHAR(45)

AS

	UPDATE [tbl_avaibility] SET [tbl_avaibility].[avaibility_SendDate]=GETDATE(),
	[tbl_avaibility].[idService]=(SELECT [tbl_service].[idService] FROM [tbl_service]	WHERE [tbl_service].[service_Name]=@service_Name)
	WHERE [tbl_avaibility].[idAvaibility]=@idAvaibility;

RETURN 0