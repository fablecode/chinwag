using Chinwag.Domain.Repository;
using MediatR;

namespace Chinwag.Application.Queries.CardCount;

public sealed class CardCountQueryHandler : IRequestHandler<CardCountQuery, int>
{
    private readonly ICardRepository _cardRepository;

    public CardCountQueryHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public Task<int> Handle(CardCountQuery request, CancellationToken cancellationToken)
    {
        return _cardRepository.Count();
    }
}