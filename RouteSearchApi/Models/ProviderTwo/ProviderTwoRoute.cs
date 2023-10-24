namespace RouteSearchApi.Models.ProviderTwo;

public class ProviderTwoRoute
{
    // Mandatory
    /// <summary>
    /// Start point of route
    /// </summary>
    public ProviderTwoPoint Departure { get; set; }


    // Mandatory
    /// <summary>
    /// End point of route
    /// </summary>
    public ProviderTwoPoint Arrival { get; set; }

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
