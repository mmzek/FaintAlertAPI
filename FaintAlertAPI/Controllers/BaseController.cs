using FaintAlertAPI.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaintAlertAPI.Controllers;

public class BaseController : ControllerBase
{
    protected ActionResult<T> ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return MapErrorToResponse(result.Error);
        }
    }

    protected ActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess)
        {
            return NoContent();
        }
        else
        {
            return MapErrorToResponse(result.Error);
        }
    }

    protected ActionResult MapErrorToResponse(Error error)
    {
        if (error.IsNone)
        {
            return Problem();
        }
        return error.Code switch
        {
            "NotFoundError" => NotFound(error.Message),
            "BadRequestError" => BadRequest(error.Message),
            "ValidationError" => BadRequest(error.Message),
            "ConflictError" => Conflict(error.Message),
            _ => Problem(detail: error.Message, title: error.Code)
        };
    }
}
