using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Helper;

public class ApiController : ControllerBase
{
    public ApiController() 
    { }

    /// <summary>
    /// Returns 200 status when <paramref name="result"/> is not null, otherwise returns 404 status.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    protected IActionResult NotFoundIfNull(object result) => result is null ? NotFound() : Ok(result);
}
