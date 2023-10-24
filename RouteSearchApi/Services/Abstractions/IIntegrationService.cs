using RouteSearchApi.Models;

namespace RouteSearchApi.Services.Abstractions;

public interface IIntegrationService
{
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
}
