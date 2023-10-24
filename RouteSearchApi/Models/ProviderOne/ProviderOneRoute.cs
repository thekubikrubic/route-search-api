namespace RouteSearchApi.Models.ProviderOne;

public class ProviderOneRoute
{
    // Mandatory
    /// <summary>
    /// Start point of route
    /// </summary>
    public string From { get; set; }

    // Mandatory
    /// <summary>
    /// End point of route
    /// </summary>
    public string To { get; set; }

    // Mandatory
    /// <summary>
    /// Start date of route
    /// </summary>
    public DateTime DateFrom { get; set; }

    // Mandatory
    /// <summary>
    /// End date of route
    /// </summary>
    public DateTime DateTo { get; set; }

    // Mandatory
    /// <summary>
    /// Price of route
    /// </summary>
    public decimal Price { get; set; }

    // Mandatory
    /// <summary>
    /// Timelimit. After it expires, route became not actual
    /// </summary>
    public DateTime TimeLimit { get; set; }
}
