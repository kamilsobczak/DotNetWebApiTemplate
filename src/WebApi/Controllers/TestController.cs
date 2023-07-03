using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/v{verion:apiVersion}/[controller]")]
[ApiController]
[AllowAnonymous]
[ApiVersion("1.0")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
        _logger.LogInformation("Test controller called");
    }

    [HttpGet("test")]
    public IActionResult Get()
    {
        string result = string.Concat("Test controller is working properly. Current date: ", DateTime.Now.ToLongTimeString());
        _logger.LogInformation(result);
        return Ok(result);
    }
}