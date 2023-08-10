namespace Chinwag.Domain.Repository;

public interface IDeckRepository
{
    Task<int> Count();
}