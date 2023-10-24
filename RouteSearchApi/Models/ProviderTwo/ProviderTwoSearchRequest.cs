namespace RouteSearchApi.Models.ProviderTwo;

public class ProviderTwoSearchRequest
{
    // Mandatory
    /// <summary>
    /// Start point of route, e.g. Moscow 
    /// </summary>
    public string Departure { get; set; }

    // Mandatory
    /// <summary>
    /// End point of route, e.g. Sochi
    /// </summary>
    public string Arrival { get; set; }

    // Mandatory
    /// <summary>
    /// Start date of route
    /// </summary>
    public DateTime DepartureDate { get; set; }

    // Optional
    /// <summary>
    /// Minimum value of timelimit for route
    /// </summary>
    public DateTime? MinTimeLimit { get; set; }
}
