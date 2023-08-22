using MediatR;

namespace Chinwag.Application.Queries.CardCount;

public record CardCountQuery : IRequest<int>
{
}