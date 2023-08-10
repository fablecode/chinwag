CREATE TABLE [dbo].[BanlistCards] (
    [BanlistId] BIGINT NOT NULL,
    [CardId]    BIGINT NOT NULL,
    [LimitId]   BIGINT NOT NULL,
    CONSTRAINT [PK_BanlistCards] PRIMARY KEY CLUSTERED ([BanlistId] ASC, [CardId] ASC),
    CONSTRAINT [FK_BanlistCards_Banlists] FOREIGN KEY ([BanlistId]) REFERENCES [dbo].[Banlists] ([Id]),
    CONSTRAINT [FK_BanlistCards_Cards] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Cards] ([Id]),
    CONSTRAINT [FK_BanlistCards_Limits] FOREIGN KEY ([LimitId]) REFERENCES [dbo].[Limits] ([Id])
);