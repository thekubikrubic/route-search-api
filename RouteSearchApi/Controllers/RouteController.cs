using Microsoft.AspNetCore.Mvc;
using RouteSearchApi.Models;
using RouteSearchApi.Services.Abstractions;

namespace RouteSearchApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{v:apiVersion}/[controller]")]
public class RouteController : ControllerBase
{
    private readonly ISearchService _searchService;
    private readonly ILogger<RouteController> _logger;

    public RouteController(
        ISearchService searchService,
        ILogger<RouteController> logger)
    {
        _searchService = searchService;
        _logger = logger;
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Route search request received");

        try
        {
            var response = await _searchService.SearchAsync(request, cancellationToken);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while serarching routes: {message}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Comment = "Internal Server Error" });
        }
    }

    [HttpGet("ping")]
    public async Task<IActionResult> Ping(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ping request received");

        try
        {
            var response = await _searchService.IsAvailableAsync(cancellationToken);

            if (response)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while ping: {message}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}