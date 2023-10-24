namespace RouteSearchApi.Models;

public class SearchResponse
{
    // Mandatory
    /// <summary>
    /// Array of routes
    /// </summary>
    public Route[] Routes { get; set; }

    // Mandatory
    /// <summary>
    /// The cheapest route
    /// </summary>
    public decimal MinPrice { get; set; }

    // Mandatory
    /// <summary>
    /// Most expensive route
    /// </summary>
    public decimal MaxPrice { get; set; }

    // Mandatory
    /// <summary>
    /// The fastest route
    /// </summary>
    public int MinMinutesRoute { get; set; }

    // Mandatory
    /// <summary>
    /// The longest route
    /// </summary>
    public int MaxMinutesRoute { get; set; }
}
