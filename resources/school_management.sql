CREATE TABLE [dbo].[Student] (
    [StudentID]      INT           IDENTITY (1, 1) NOT NULL,
    [LastName]       NVARCHAR (50) NOT NULL,
    [FirstName]      NVARCHAR (50) NOT NULL,
    [EnrollmentDate] DATETIME      NOT NULL,
	[MiddleName]	 NVARCHAR (50) NULL,
	[Hobby]			 NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([StudentID] ASC)
)

CREATE TABLE [dbo].[Course] (
    [CourseID] INT           IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (50) NOT NULL,
    [Credits]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([CourseID] ASC)
)

CREATE TABLE [dbo].[Lecturer] (
    [LecturerID] 	INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [LastName]  	NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([LecturerID] ASC)
)

CREATE TABLE [dbo].[Enrollment] (
    [EnrollmentID] INT IDENTITY (1, 1) NOT NULL,
    [Grade]        DECIMAL(3, 2) NULL,
    [CourseID]     INT NOT NULL,
    [StudentID]    INT NOT NULL,
	[LecturerID]   INT NULL,
    PRIMARY KEY CLUSTERED ([EnrollmentID] ASC),
    CONSTRAINT [FK_dbo.Enrollment_dbo.Course_CourseID] FOREIGN KEY ([CourseID]) 
        REFERENCES [dbo].[Course] ([CourseID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Enrollment_dbo.Student_StudentID] FOREIGN KEY ([StudentID]) 
        REFERENCES [dbo].[Student] ([StudentID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Enrollment_dbo.Lecturer_LecturerID] FOREIGN KEY ([LecturerID]) 
        REFERENCES [dbo].[Lecturer] ([LecturerID]) ON DELETE CASCADE
)