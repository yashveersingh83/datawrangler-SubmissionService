using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System.Threading;
using System.Threading.Tasks;

namespace SubmissionService.Application.Features.Cache.Query
{
    public class DeleteAllCacheQuery : IRequest<bool> { }

    public class DeleteAllCacheQueryHandler : IRequestHandler<DeleteAllCacheQuery, bool>
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly ILogger<DeleteAllCacheQueryHandler> _logger;

        public DeleteAllCacheQueryHandler(IRedisCacheService redisCacheService, ILogger<DeleteAllCacheQueryHandler> logger)
        {
            _redisCacheService = redisCacheService;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteAllCacheQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting all cache...");
                await _redisCacheService.RemoveCacheAsync(CacheKeyConstant.RecipientKey); 
                await _redisCacheService.RemoveCacheAsync(CacheKeyConstant.ManagerCacheKey); 
                await _redisCacheService.RemoveCacheAsync(CacheKeyConstant.OrgUnitKey); 
                await _redisCacheService.RemoveCacheAsync(CacheKeyConstant.RequestStatusKey); 
                await _redisCacheService.RemoveCacheAsync(CacheKeyConstant.MileStoneKey);
                await _redisCacheService.RemoveCacheAsync(CacheKeyConstant.SubmissionTypeKey);
                _logger.LogInformation("All cache deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete all cache: {Message}", ex.Message);
                return false;
            }
        }
    }
}
