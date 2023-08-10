CREATE TABLE [dbo].[SubCategories] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CategoryId] BIGINT         NOT NULL,
    [Name]       NVARCHAR (255) NOT NULL,
    [Created]    DATETIME2 (7)  NOT NULL,
    [Updated]    DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_SubCategories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubCategories_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
);