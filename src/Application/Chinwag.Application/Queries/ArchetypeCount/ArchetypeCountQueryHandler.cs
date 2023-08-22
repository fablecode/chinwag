using Chinwag.Domain.Repository;
using MediatR;

namespace Chinwag.Application.Queries.ArchetypeCount;

public sealed class ArchetypeCountQueryHandler : IRequestHandler<ArchetypeCountQuery, int>
{
    private readonly IArchetypeRepository _archetypeRepository;

    public ArchetypeCountQueryHandler(IArchetypeRepository archetypeRepository)
    {
        _archetypeRepository = archetypeRepository;
    }
    public Task<int> Handle(ArchetypeCountQuery request, CancellationToken cancellationToken)
    {
        return _archetypeRepository.Count();
    }
}