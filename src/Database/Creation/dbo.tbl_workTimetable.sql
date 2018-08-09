CREATE TABLE [dbo].[tbl_workTimetable] (
    [idWorkTimetable]     INT      IDENTITY (1, 1) NOT NULL,
    [workTimetable_Start] TIME (0) NOT NULL,
    [workTimetable_End]   TIME (0) NOT NULL,
    CONSTRAINT [PK_tbl_workTimetable_idWorkTimetable] PRIMARY KEY CLUSTERED ([idWorkTimetable] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_workTimetable_idWorkTimetable]
    ON [dbo].[tbl_workTimetable]([idWorkTimetable] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tbl_workTimetable_workTimetable_Start_workTimetable_End]
    ON [dbo].[tbl_workTimetable]([workTimetable_Start] ASC, [workTimetable_End] ASC);

