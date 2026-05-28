using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Presentation.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : BaseApiController
    {
        [Route("errors/{code}")]
        public ActionResult Error(int code)
        {
            return code switch
            {
                401 => Unauthorized(new ApiResponse(401)),
                404 => NotFound(new ApiResponse(404)),
                _ => StatusCode(code, new ApiResponse(code))
            };
        }
    }
}
