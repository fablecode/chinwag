﻿CREATE TABLE [dbo].[DeckTypes] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (255) NOT NULL,
    [Created] DATETIME2 (7)  NOT NULL,
    [Updated] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_DeckTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);