using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid userId =>Guid.NewGuid(); //new (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
    }
}
