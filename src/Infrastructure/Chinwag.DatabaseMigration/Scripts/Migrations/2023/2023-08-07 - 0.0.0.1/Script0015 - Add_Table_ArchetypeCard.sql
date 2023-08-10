CREATE TABLE [dbo].[ArchetypeCards] (
    [ArchetypeId] BIGINT NOT NULL,
    [CardId]      BIGINT NOT NULL,
    CONSTRAINT [PK_ArchetypeCards] PRIMARY KEY CLUSTERED ([ArchetypeId] ASC, [CardId] ASC),
    CONSTRAINT [FK_ArchetypeCards_Archetypes] FOREIGN KEY ([ArchetypeId]) REFERENCES [dbo].[Archetypes] ([Id]) NOT FOR REPLICATION,
    CONSTRAINT [FK_ArchetypeCards_Cards] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Cards] ([Id]) NOT FOR REPLICATION
);