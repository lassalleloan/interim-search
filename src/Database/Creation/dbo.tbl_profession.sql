CREATE TABLE [dbo].[tbl_profession] (
    [idProfession]    INT           IDENTITY (1, 1) NOT NULL,
    [profession_Name] NVARCHAR (45) NOT NULL,
    CONSTRAINT [PK_tbl_profession_idProfession] PRIMARY KEY CLUSTERED ([idProfession] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_profession_idProfession]
    ON [dbo].[tbl_profession]([idProfession] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_profession_profession_Name]
    ON [dbo].[tbl_profession]([profession_Name] ASC);

