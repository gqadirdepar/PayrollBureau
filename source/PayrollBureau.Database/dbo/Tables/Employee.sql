CREATE TABLE [dbo].[Employee] (
    [EmployeeId]     INT            IDENTITY (1, 1) NOT NULL,
    [EmployerId]     INT            NOT NULL,
    [Name]           NVARCHAR (100) NOT NULL,
    [Address1]       NVARCHAR (100) NULL,
    [Address2]       NVARCHAR (100) NULL,
    [Address3]       NVARCHAR (100) NULL,
    [Address4]       NVARCHAR (100) NULL,
    [EmployeeCode]   INT            NULL,
    [PayrollId]      INT            NULL,
    [EmailId]        NVARCHAR (256) NOT NULL,
    [CreatedDateUtc] DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (128) NULL,
    [AspnetUserId]   NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    CONSTRAINT [FK_Employee_Employer] FOREIGN KEY ([EmployerId]) REFERENCES [dbo].[Employer] ([EmployerId])
);

