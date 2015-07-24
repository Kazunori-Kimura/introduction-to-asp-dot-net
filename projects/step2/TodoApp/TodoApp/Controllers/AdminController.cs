using System.Web.Mvc;

namespace TodoApp.Controllers
{
    [Authorize(Roles="Administrators")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}