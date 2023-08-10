CREATE TABLE [dbo].[Decks] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255)  NOT NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [VideoUrl]    NVARCHAR (2083) NULL,
    [Created]     DATETIME2 (7)   NOT NULL,
    [Updated]     DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_Decks] PRIMARY KEY CLUSTERED ([Id] ASC)
);