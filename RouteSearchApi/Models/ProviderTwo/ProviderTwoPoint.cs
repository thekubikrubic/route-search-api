namespace RouteSearchApi.Models.ProviderTwo;

public class ProviderTwoPoint
{
    // Mandatory
    /// <summary>
    /// Name of point, e.g. Moscow\Sochi
    /// </summary>
    public string Point { get; set; }

    // Mandatory
    /// <summary>
    /// Date for point in Route, e.g. Point = Moscow, Date = 2023-01-01 15-00-00
    /// </summary>
    public DateTime Date { get; set; }
}
