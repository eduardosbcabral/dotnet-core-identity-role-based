using AspNetCoreIdentity.Extensions;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //net Users
        //UserRoles
        //RoleClaims

       
        public IActionResult Privacy()
        {
            return View();
        }

        //[ClaimsAuthorize("Administrador", "Visualizar")]
        public IActionResult Secret()
        {
            return View();
        }

        //[ClaimsAuthorize("Vendedor", "Excluir")]
        public IActionResult Vendedor()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
