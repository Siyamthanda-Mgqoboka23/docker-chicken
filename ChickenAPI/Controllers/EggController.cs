using Microsoft.AspNetCore.Mvc;

namespace ChickenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EggController : ControllerBase
    {
        [HttpGet("/GetMeEgggs")]
        public ActionResult<string> GetEggs()
        {
            return "Here are your eggs!";
        }
    }
}