CREATE TABLE [dbo].[EmployeeDocument] (
    [EmployeeDocumentId] INT              NOT NULL,
    [EmployeeId]         INT              NOT NULL,
    [DocumentCategoryId] INT              NOT NULL,
    [DocumentGuid]       UNIQUEIDENTIFIER NOT NULL,
    [ValidFromDate]      DATETIME         NULL,
    [ValidToDate]        DATETIME         NULL,
    [CreatedBy]          NVARCHAR (128)   COLLATE Latin1_General_CI_AS NULL,
    [CreatedDateUtc]     DATETIME2 (7)    CONSTRAINT [DF_EmployeeDocument_CreatedDateUtc] DEFAULT (getutcdate()) NULL,
    CONSTRAINT [PK_EmployeeDocument] PRIMARY KEY CLUSTERED ([EmployeeDocumentId] ASC),
    CONSTRAINT [FK_EmployeeDocument_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId])
);

