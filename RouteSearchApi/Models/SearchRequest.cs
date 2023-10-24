namespace RouteSearchApi.Models;

public class SearchRequest
{
    // Mandatory
    /// <summary>
    /// Start point of route, e.g. Moscow 
    /// </summary>
    public string Origin { get; set; }

    // Mandatory
    /// <summary>
    /// End point of route, e.g. Sochi
    /// </summary>
    public string Destination { get; set; }

    // Mandatory
    /// <summary>
    /// Start date of route
    /// </summary>
    public DateTime OriginDateTime { get; set; }

    // Optional
    public SearchFilters? Filters { get; set; }
}
