using Microsoft.AspNetCore.Mvc;
using WebAPI.Attributes;

namespace WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PublicController : ControllerBase { }
}