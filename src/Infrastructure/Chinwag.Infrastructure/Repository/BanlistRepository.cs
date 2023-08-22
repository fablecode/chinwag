using Chinwag.Domain.Repository;
using Chinwag.Infrastructure.Database;
using Chinwag.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinwag.Infrastructure.Repository;

public class BanlistRepository : IBanlistRepository
{
    private readonly ChinwagDbContext _dbContext;

    public BanlistRepository(ChinwagDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> Count()
    {
        IQueryable<Banlist> banlists = _dbContext.Banlists;

        return banlists.CountAsync();
    }
}