CREATE TABLE [dbo].[tbl_country] (
    [idCountry]    INT           IDENTITY (1, 1) NOT NULL,
    [country_Name] NVARCHAR (45) NOT NULL,
    CONSTRAINT [PK_tbl_country_idCountry] PRIMARY KEY CLUSTERED ([idCountry] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_country_idCountry]
    ON [dbo].[tbl_country]([idCountry] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_country_counrty_Name]
    ON [dbo].[tbl_country]([country_Name] ASC);

