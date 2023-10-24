namespace RouteSearchApi.Models;

public class SearchFilters
{
    // Optional
    /// <summary>
    /// End date of route
    /// </summary>
    public DateTime? DestinationDateTime { get; set; }

    // Optional
    /// <summary>
    /// Maximum price of route
    /// </summary>
    public decimal? MaxPrice { get; set; }

    // Optional
    /// <summary>
    /// Minimum value of timelimit for route
    /// </summary>
    public DateTime? MinTimeLimit { get; set; }

    // Optional
    /// <summary>
    /// Forcibly search in cached data
    /// </summary>
    public bool? OnlyCached { get; set; }
}
