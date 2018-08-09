CREATE TABLE [dbo].[tbl_service] (
    [idService]    INT           IDENTITY (1, 1) NOT NULL,
    [service_Name] NVARCHAR (45) NOT NULL,
    CONSTRAINT [PK_tbl_service_idService] PRIMARY KEY CLUSTERED ([idService] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_service_idService]
    ON [dbo].[tbl_service]([idService] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_service_service_Name]
    ON [dbo].[tbl_service]([service_Name] ASC);

