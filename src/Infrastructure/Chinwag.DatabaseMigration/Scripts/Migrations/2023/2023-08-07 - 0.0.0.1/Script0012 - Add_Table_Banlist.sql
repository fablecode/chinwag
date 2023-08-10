CREATE TABLE [dbo].[Banlists] (
    [Id]          BIGINT         NOT NULL,
    [FormatId]    BIGINT         NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [ReleaseDate] DATETIME       NOT NULL,
    [Created]     DATETIME2 (7)  NOT NULL,
    [Updated]     DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Banlists] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Banlists_Format] FOREIGN KEY ([FormatId]) REFERENCES [dbo].[Formats] ([Id]) NOT FOR REPLICATION
);