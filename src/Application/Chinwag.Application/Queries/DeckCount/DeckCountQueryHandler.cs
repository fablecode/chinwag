using Chinwag.Domain.Repository;
using MediatR;

namespace Chinwag.Application.Queries.DeckCount;

public sealed class DeckCountQueryHandler : IRequestHandler<DeckCountQuery, int>
{
    private readonly IDeckRepository _deckRepository;

    public DeckCountQueryHandler(IDeckRepository deckRepository)
    {
        _deckRepository = deckRepository;
    }
    public Task<int> Handle(DeckCountQuery request, CancellationToken cancellationToken)
    {
        return _deckRepository.Count();
    }
}