using Chinwag.Domain.Repository;
using Chinwag.Infrastructure.Database;
using Chinwag.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinwag.Infrastructure.Repository;

public class ArchetypeRepository : IArchetypeRepository
{
    private readonly ChinwagDbContext _dbContext;

    public ArchetypeRepository(ChinwagDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> Count()
    {
        IQueryable<Archetype> archetypes = _dbContext.Archetypes;

        return archetypes.CountAsync();
    }
}