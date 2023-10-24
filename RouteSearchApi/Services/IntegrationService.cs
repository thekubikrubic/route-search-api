using AutoMapper;
using RouteSearchApi.Models;
using RouteSearchApi.Services.Abstractions;

namespace RouteSearchApi.Services;

public class IntegrationService: IIntegrationService
{
    protected virtual string IntegrationUrl => "";

    protected static HttpClient HttpClient;

    protected readonly IMapper Mapper;

    public IntegrationService(IMapper mapper)
    {
        Mapper = mapper;
        HttpClient = new() { BaseAddress = new Uri(IntegrationUrl) };
    }

    public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await HttpClient.GetAsync("ping", cancellationToken);

        return response.IsSuccessStatusCode;
    }

    public virtual Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
