CREATE TABLE [dbo].[tbl_employmentAgency] (
    [idEmploymentAgency]           INT            IDENTITY (1, 1) NOT NULL,
    [employmentAgency_Name]        NVARCHAR (45)  NOT NULL,
    [employmentAgency_Street]      NVARCHAR (45)  DEFAULT (NULL) NULL,
    [employmentAgency_Number]      TINYINT        DEFAULT (NULL) NULL,
    [employmentAgency_PostCode]    NVARCHAR (9)   DEFAULT (NULL) NULL,
    [idCity]                       INT            NOT NULL,
    [employmentAgency_EmailAdress] NVARCHAR (100) DEFAULT (NULL) NULL,
    [employmentAgency_PhoneNumber] BIGINT         DEFAULT (NULL) NULL,
    CONSTRAINT [PK_tbl_employmentAgency_idEmploymentAgency] PRIMARY KEY CLUSTERED ([idEmploymentAgency] ASC),
    CONSTRAINT [FK_tbl_employmentAgency_idCity] FOREIGN KEY ([idCity]) REFERENCES [dbo].[tbl_city] ([idCity])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_employmentAgency_idEmploymentAgency]
    ON [dbo].[tbl_employmentAgency]([idEmploymentAgency] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_employmentAgency_employmentAgency_Name]
    ON [dbo].[tbl_employmentAgency]([employmentAgency_Name], [idCity] ASC);

