CREATE TABLE [dbo].[Bureau] (
    [BureauId]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (100) NOT NULL,
    [Address1]       NVARCHAR (100) NULL,
    [Address2]       NVARCHAR (100) NULL,
    [Address3]       NVARCHAR (100) NULL,
    [Address4]       NVARCHAR (100) NULL,
    [EmailId]        NVARCHAR (256) NOT NULL,
    [CreatedDateUtc] DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (128) NULL,
    [AspnetUserId]   NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_Bureau] PRIMARY KEY CLUSTERED ([BureauId] ASC),
    CONSTRAINT [FK_Bureau_Bureau] FOREIGN KEY ([BureauId]) REFERENCES [dbo].[Bureau] ([BureauId])
);

