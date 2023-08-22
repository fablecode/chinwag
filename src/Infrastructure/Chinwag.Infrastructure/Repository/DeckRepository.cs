﻿using Chinwag.Domain.Repository;
using Chinwag.Infrastructure.Database;
using Chinwag.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinwag.Infrastructure.Repository;

public class DeckRepository : IDeckRepository
{
    private readonly ChinwagDbContext _dbContext;

    public DeckRepository(ChinwagDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> Count()
    {
        IQueryable<Deck> decks = _dbContext.Decks;

        return decks.CountAsync();
    }
}