CREATE TABLE [dbo].[tbl_region] (
    [idRegion]    INT           IDENTITY (1, 1) NOT NULL,
    [region_Name] NVARCHAR (45) NOT NULL,
    [idCountry]   INT           DEFAULT (NULL) NULL,
    CONSTRAINT [PK_tbl_region_idRegion] PRIMARY KEY CLUSTERED ([idRegion] ASC),
    CONSTRAINT [FK_tbl_region_idCountry] FOREIGN KEY ([idCountry]) REFERENCES [dbo].[tbl_country] ([idCountry])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_region_idRegion]
    ON [dbo].[tbl_region]([idRegion] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_region_region_Name]
    ON [dbo].[tbl_region]([region_Name] ASC);

