namespace RouteSearchApi.Models;

public class Route
{
    // Mandatory
    // В идеале бы сделать уникальный ключ на основе других полей, чтобы не кешировать дубликаты
    /// <summary>
    /// Identifier of the whole route
    /// </summary>
    public Guid Id { get; set; }

    // Mandatory
    /// <summary>
    /// Start point of route
    /// </summary>
    public string Origin { get; set; }

    // Mandatory
    /// <summary>
    /// End point of route
    /// </summary>
    public string Destination { get; set; }

    // Mandatory
    /// <summary>
    /// Start date of route
    /// </summary>
    public DateTime OriginDateTime { get; set; }

    // Mandatory
    /// <summary>
    /// End date of route
    /// </summary>
    public DateTime DestinationDateTime { get; set; }

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
