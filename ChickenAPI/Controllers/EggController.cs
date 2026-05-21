using Microsoft.AspNetCore.Mvc;

namespace ChickenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EggController : ControllerBase
    {
        [HttpGet("/GetMeEggs")]
        public ActionResult GetEggs()
        {
            return Ok("You got some eggs!");
        }


    }
}