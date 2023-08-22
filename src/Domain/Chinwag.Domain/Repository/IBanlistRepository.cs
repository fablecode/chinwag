namespace Chinwag.Domain.Repository;

public interface IBanlistRepository
{
    Task<int> Count();
}