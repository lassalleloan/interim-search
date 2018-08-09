CREATE TABLE [dbo].[tbl_temporary] (
    [idTemporary]                   INT            IDENTITY (1, 1) NOT NULL,
    [temporary_Title]               NVARCHAR (5)   DEFAULT (NULL) NULL,
    [temporary_Name]                NVARCHAR (45)  NOT NULL,
    [temporary_FirstName]           NVARCHAR (45)  DEFAULT (NULL) NULL,
    [temporary_BirthDate]           DATE           DEFAULT (NULL) NULL,
    [temporary_Street]              NVARCHAR (45)  DEFAULT (NULL) NULL,
    [temporary_Number]              TINYINT        DEFAULT (NULL) NULL,
    [temporary_PostCode]            NVARCHAR (9)   DEFAULT (NULL) NULL,
    [idCity]                        INT            NOT NULL,
    [temporary_EmailAdress]         NVARCHAR (100) DEFAULT (NULL) NULL,
    [temporary_PhoneNumber]         BIGINT         DEFAULT (NULL) NULL,
    [temporary_Certified]           BIT            DEFAULT (NULL) NULL,
    [temporary_CertificationDate]   DATE           DEFAULT (NULL) NULL,
    [idProfession]                  INT            NOT NULL,
    [temporary_StartDateExperience] DATE           DEFAULT (NULL) NULL,
    [idEmploymentAgency]            INT            NOT NULL,
    CONSTRAINT [PK_tbl_temporary_idTemporary] PRIMARY KEY CLUSTERED ([idTemporary] ASC),
    CONSTRAINT [FK_tbl_temporary_idCity] FOREIGN KEY ([idCity]) REFERENCES [dbo].[tbl_city] ([idCity]),
    CONSTRAINT [FK_tbl_temporary_idProfession] FOREIGN KEY ([idProfession]) REFERENCES [dbo].[tbl_profession] ([idProfession]),
    CONSTRAINT [FK_tbl_temporary_idEmploymentAgency] FOREIGN KEY ([idEmploymentAgency]) REFERENCES [dbo].[tbl_employmentAgency] ([idEmploymentAgency]),
    CONSTRAINT [CK_tbl_temporary_Title] CHECK ([temporary_Title]='M.' OR [temporary_Title]='Mme.' OR [temporary_Title]='Mlle.')
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_temporary_idTemporary]
    ON [dbo].[tbl_temporary]([idTemporary] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_temporary_Name_temporary_FirstNametemporary_BirthDate]
    ON [dbo].[tbl_temporary]([temporary_Name] ASC, [temporary_FirstName] ASC, [temporary_BirthDate] ASC);

