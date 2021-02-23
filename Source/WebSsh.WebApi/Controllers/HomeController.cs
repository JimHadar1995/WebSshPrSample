using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSsh.WebApi.Controllers
{
    /// <summary>
    /// Вспомогательный контроллер для редиректа с серверного на клиентский роутинг
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Редирект на index.html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Index()
            => RedirectPermanent("/swagger");
    }
}
