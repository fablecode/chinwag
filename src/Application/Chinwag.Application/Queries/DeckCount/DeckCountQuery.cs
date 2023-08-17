using MediatR;

namespace Chinwag.Application.Queries.DeckCount;

public record DeckCountQuery : IRequest<int>
{
}