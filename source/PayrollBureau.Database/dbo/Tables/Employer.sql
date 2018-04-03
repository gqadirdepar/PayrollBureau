CREATE TABLE [dbo].[Employer] (
    [EmployerId]     INT            IDENTITY (1, 1) NOT NULL,
    [BureauId]       INT            NOT NULL,
    [Name]           NVARCHAR (100) NOT NULL,
    [Address1]       NVARCHAR (100) NULL,
    [Address2]       NVARCHAR (100) NULL,
    [Address3]       NVARCHAR (100) NULL,
    [Address4]       NVARCHAR (100) NULL,
    [Email]          NVARCHAR (256) NOT NULL,
    [CreatedDateUtc] DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (128) NULL,
    [AspnetUserId]   NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_Employer] PRIMARY KEY CLUSTERED ([EmployerId] ASC),
    CONSTRAINT [FK_Employer_Bureau] FOREIGN KEY ([EmployerId]) REFERENCES [dbo].[Bureau] ([BureauId]),
    CONSTRAINT [FK_Employer_Employer] FOREIGN KEY ([EmployerId]) REFERENCES [dbo].[Employer] ([EmployerId])
);

