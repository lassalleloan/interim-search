CREATE TABLE [dbo].[tbl_avaibility] (
    [idAvaibility]               INT  IDENTITY (1, 1) NOT NULL,
    [avaibility_Date]            DATE NOT NULL,
    [idWorkTimetable]            INT  NOT NULL,
    [idTemporary]                INT  NOT NULL,
    [idService]                  INT  DEFAULT (NULL) NULL,
    [avaibility_SendDate]        DATE DEFAULT (NULL) NULL,
    [avaibility_AcceptedMission] BIT  DEFAULT (NULL) NULL,
    CONSTRAINT [PK_tbl_avaibility_idAvaibility] PRIMARY KEY CLUSTERED ([idAvaibility] ASC),
    CONSTRAINT [FK_tbl_avaibility_idWorkTimetable] FOREIGN KEY ([idWorkTimetable]) REFERENCES [dbo].[tbl_workTimetable] ([idWorkTimetable]),
    CONSTRAINT [FK_tbl_avaibility_idTemporary] FOREIGN KEY ([idTemporary]) REFERENCES [dbo].[tbl_temporary] ([idTemporary]),
    CONSTRAINT [FK_tbl_avaibility_idService] FOREIGN KEY ([idService]) REFERENCES [dbo].[tbl_service] ([idService])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_avaibility_idAvaibility]
    ON [dbo].[tbl_avaibility]([idAvaibility] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_avaibility_avaibility_Date_idWorkTimetable_idTemporary]
    ON [dbo].[tbl_avaibility]([avaibility_Date] ASC, [idWorkTimetable] ASC, [idTemporary] ASC);

