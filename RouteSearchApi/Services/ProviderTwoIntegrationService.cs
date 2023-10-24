using AutoMapper;
using RouteSearchApi.Models;
using RouteSearchApi.Models.ProviderTwo;
using System.Text;
using System.Text.Json;
using Route = RouteSearchApi.Models.Route;

namespace RouteSearchApi.Services;

public class ProviderTwoIntegrationService : IntegrationService
{
    protected override string IntegrationUrl => "http://provider-two/api/v1";

    public ProviderTwoIntegrationService(IMapper mapper) : base(mapper)
    {
    }

    public override async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        var providerRequest = Mapper.Map<ProviderTwoSearchRequest>(request);

        using StringContent jsonContent = new(
            JsonSerializer.Serialize(providerRequest),
            Encoding.UTF8,
            "application/json"
        );
        using HttpResponseMessage response = await HttpClient.PostAsync("search", jsonContent, cancellationToken);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var providerResponse = JsonSerializer.Deserialize<ProviderTwoSearchResponse>(jsonResponse);

        if (providerResponse == null)
        {
            return new SearchResponse();
        }

        var routes = Mapper.Map<Route[]>(providerResponse.Routes);

        if (request.Filters != null)
        {
            if (request.Filters.DestinationDateTime != null)
            {
                routes = routes.Where(x => x.DestinationDateTime <= request.Filters.DestinationDateTime).ToArray();
            }
            if (request.Filters.MaxPrice != null)
            {
                routes = routes.Where(x => x.Price <= request.Filters.MaxPrice).ToArray();
            }
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
