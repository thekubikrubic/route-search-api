using Microsoft.Extensions.Caching.Memory;
using RouteSearchApi.Models;
using RouteSearchApi.Services.Abstractions;
using System.Collections;
using System.Reflection;
using Route = RouteSearchApi.Models.Route;

namespace RouteSearchApi.Services;

public class SearchService : ISearchService
{
    private IEnumerable<IIntegrationService> _integrationServices;
    private readonly IMemoryCache _cache;

    private readonly object _cacheLock;

    public SearchService(
        IEnumerable<IIntegrationService> integrationServices,
        IMemoryCache cache)
    {
        _integrationServices = integrationServices;
        _cache = cache;
    }

    public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
    {
        // Думал тут пинговать всех провайдеров и возвращать false если не один не работает
        // Но тогда можем ввести пользователя в заблужднеие, так как поиск по кэшу будет продолжать работать
        return true;
    }

    public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        if (request.Filters != null && request.Filters.OnlyCached == true)
        {
            return SearchInCache(request);
        }

        var tasks = new List<Task<SearchResponse>>();
        foreach (var integration in _integrationServices)
        {
            if (await integration.IsAvailableAsync(cancellationToken))
            {
                tasks.Add(integration.SearchAsync(request, cancellationToken));
            }
        }

        var responses = await Task.WhenAll(tasks);

        var result = new SearchResponse
        {
            Routes = responses.SelectMany(x => x.Routes).ToArray(),
            MinPrice = responses.Select(x => x.MinPrice).Min(),
            MaxPrice = responses.Select(x => x.MaxPrice).Max(),
            MinMinutesRoute = responses.Select(x => x.MinMinutesRoute).Min(),
            MaxMinutesRoute = responses.Select(x => x.MaxMinutesRoute).Max(),
        };

        AddToCache(result.Routes);

        return result;
    }

    private void AddToCache(Route[] routes)
    {
        foreach (var route in routes)
        {
            if (_cache.TryGetValue(route.Id, out _))
            {
                continue;
            }

            lock (_cacheLock)
            {
                _cache.Set(route.Id, route, route.TimeLimit);
            }
        }
    }

    private SearchResponse SearchInCache(SearchRequest request)
    {
        var routes = GetAllCacheValues()
            .Where(x => x.Origin == request.Origin)
            .Where(x => x.Destination == request.Destination)
            .Where(x => x.OriginDateTime == request.OriginDateTime);

        if (request.Filters != null)
        {
            if (request.Filters.DestinationDateTime != null)
            {
                routes = routes.Where(x => x.DestinationDateTime == request.Filters.DestinationDateTime);
            }
            if (request.Filters.MaxPrice != null)
            {
                routes = routes.Where(x => x.Price <= request.Filters.MaxPrice);
            }
            if (request.Filters.MinTimeLimit != null)
            {
                routes = routes.Where(x => x.TimeLimit >= request.Filters.MinTimeLimit);
            }
        }

        var prices = routes.Select(x => x.Price);
        var durations = routes.Select(x => (x.DestinationDateTime - x.OriginDateTime).Minutes);

        var result = new SearchResponse
        {
            Routes = routes.ToArray(),
            MinPrice = prices.Max(),
            MinMinutesRoute = durations.Min(),
            MaxMinutesRoute = durations.Max()
        };

        return result;
    }

    private List<Route> GetAllCacheValues()
    {
        var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
        var collection = field.GetValue(_cache) as ICollection;

        var items = new List<Route>();

        if (collection != null)
        {
            foreach (var item in collection)
            {
                var methodInfo = item.GetType().GetProperty("Value");
                var val = methodInfo.GetValue(item);
                items.Add((Route)val);
            }
        }

        return items;
    }
}
