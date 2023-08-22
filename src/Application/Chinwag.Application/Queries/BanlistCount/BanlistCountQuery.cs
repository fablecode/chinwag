using MediatR;

namespace Chinwag.Application.Queries.BanlistCount;

public record BanlistCountQuery : IRequest<int>
{
}