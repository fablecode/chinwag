using Chinwag.Domain.Repository;
using MediatR;

namespace Chinwag.Application.Queries.BanlistCount;

public sealed class BanlistCountQueryHandler : IRequestHandler<BanlistCountQuery, int>
{
    private readonly IBanlistRepository _banlistRepository;

    public BanlistCountQueryHandler(IBanlistRepository banlistRepository)
    {
        _banlistRepository = banlistRepository;
    }
    public Task<int> Handle(BanlistCountQuery request, CancellationToken cancellationToken)
    {
        return _banlistRepository.Count();
    }
}