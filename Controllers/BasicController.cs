using Microsoft.AspNetCore.Mvc;


namespace Szkolenie_Kamsoft.Controllers
{
    public record PersonRequest(Guid Id, string name, string Surname)
    {

    }
    public class BasicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
