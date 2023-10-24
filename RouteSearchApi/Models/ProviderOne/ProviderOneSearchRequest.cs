namespace RouteSearchApi.Models.ProviderOne;

public class ProviderOneSearchRequest
{
    // Mandatory
    /// <summary>
    /// Start point of route, e.g. Moscow 
    /// </summary>
    public string From { get; set; }

    // Mandatory
    /// <summary>
    /// End point of route, e.g. Sochi
    /// </summary>
    public string To { get; set; }

    // Mandatory
    /// <summary>
    /// Start date of route
    /// </summary>
    public DateTime DateFrom { get; set; }

    // Optional
    /// <summary>
    /// End date of route
    /// </summary>
    public DateTime? DateTo { get; set; }

    // Optional
    /// <summary>
    /// Maximum price of route
    /// </summary>
    public decimal? MaxPrice { get; set; }
}
