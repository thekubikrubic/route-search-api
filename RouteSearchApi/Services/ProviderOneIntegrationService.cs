using AutoMapper;
using RouteSearchApi.Models;
using RouteSearchApi.Models.ProviderOne;
using System.Text;
using System.Text.Json;
using Route = RouteSearchApi.Models.Route;

namespace RouteSearchApi.Services;

public class ProviderOneIntegrationService : IntegrationService
{
    protected override string IntegrationUrl => "http://provider-one/api/v1";

    public ProviderOneIntegrationService(IMapper mapper) : base(mapper)
    {
    }

    public override async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        var providerRequest = Mapper.Map<ProviderOneSearchRequest>(request);

        using StringContent jsonContent = new(
            JsonSerializer.Serialize(providerRequest),
            Encoding.UTF8,
            "application/json"
        );
        using HttpResponseMessage response = await HttpClient.PostAsync("search", jsonContent, cancellationToken);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var providerResponse = JsonSerializer.Deserialize<ProviderOneSearchResponse>(jsonResponse);

        if (providerResponse == null)
        {
            return new SearchResponse();
        }

        var routes = Mapper.Map<Route[]>(providerResponse.Routes);

        if (request.Filters != null && request.Filters.MinTimeLimit != null)
        {
            routes = routes.Where(x => x.TimeLimit >= request.Filters.MinTimeLimit).ToArray();
        }

        var prices = routes.Select(x => x.Price);
        var durations = routes.Select(x => (x.DestinationDateTime - x.OriginDateTime).Minutes);

        var result = new SearchResponse
        {
            Routes = routes,
            MinPrice = prices.Min(),
            MaxPrice = prices.Min(),
            MinMinutesRoute = durations.Min(),
            MaxMinutesRoute = durations.Max()
        };

        return result;
    }
}
