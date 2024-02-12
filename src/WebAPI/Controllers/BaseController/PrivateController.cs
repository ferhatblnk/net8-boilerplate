using Microsoft.AspNetCore.Mvc;
using WebAPI.Attributes;

namespace WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [JWTAuth]
    public class PrivateController : ControllerBase { }
}