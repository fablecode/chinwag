CREATE TABLE [dbo].[DeckCards] (
    [DeckTypeId] BIGINT NOT NULL,
    [DeckId]     BIGINT NOT NULL,
    [CardId]     BIGINT NOT NULL,
    [Quantity]   INT    NOT NULL,
    [SortOrder]  INT    NOT NULL,
    CONSTRAINT [PK_DeckCards] PRIMARY KEY CLUSTERED ([DeckTypeId] ASC, [DeckId] ASC, [CardId] ASC),
    CONSTRAINT [FK_DeckCards_Cards] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Cards] ([Id]),
    CONSTRAINT [FK_DeckCards_Decks] FOREIGN KEY ([DeckId]) REFERENCES [dbo].[Decks] ([Id]),
    CONSTRAINT [FK_DeckCards_DeckTypes] FOREIGN KEY ([DeckTypeId]) REFERENCES [dbo].[DeckTypes] ([Id])
);