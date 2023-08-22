using Chinwag.Domain.Repository;
using Chinwag.Infrastructure.Database;
using Chinwag.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinwag.Infrastructure.Repository;

public class CardRepository : ICardRepository
{
    private readonly ChinwagDbContext _dbContext;

    public CardRepository(ChinwagDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> Count()
    {
        IQueryable<Card> cards = _dbContext.Cards;

        return cards.CountAsync();
    }
}