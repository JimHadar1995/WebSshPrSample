using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Редирект на index.html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Index()
        //=> RedirectPermanent("/swagger");
        {
            return PhysicalFile(Path.Combine(_env.WebRootPath, "index.html"), "text/html");
        }
    }
}
