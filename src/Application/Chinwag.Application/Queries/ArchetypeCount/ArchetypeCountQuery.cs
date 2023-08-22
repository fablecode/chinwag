using MediatR;

namespace Chinwag.Application.Queries.ArchetypeCount;

public record ArchetypeCountQuery : IRequest<int>
{
}