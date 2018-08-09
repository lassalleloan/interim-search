CREATE TABLE [dbo].[tbl_city] (
    [idCity]    INT           IDENTITY (1, 1) NOT NULL,
    [city_Name] NVARCHAR (45) NOT NULL,
    [idRegion]  INT           DEFAULT (NULL) NULL,
    CONSTRAINT [PK_tbl_city_idCity] PRIMARY KEY CLUSTERED ([idCity] ASC),
    CONSTRAINT [FK_tbl_city_idRegion] FOREIGN KEY ([idRegion]) REFERENCES [dbo].[tbl_region] ([idRegion])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_city_idCity]
    ON [dbo].[tbl_city]([idCity] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_city_city_Name]
    ON [dbo].[tbl_city]([city_Name] ASC);

