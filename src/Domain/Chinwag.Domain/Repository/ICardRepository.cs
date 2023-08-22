namespace Chinwag.Domain.Repository;

public interface ICardRepository
{
    Task<int> Count();
}