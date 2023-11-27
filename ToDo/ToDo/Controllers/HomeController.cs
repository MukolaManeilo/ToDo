using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToDoContext _context;

        public HomeController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Tasks> tasks = _context.Tasks.ToList();
            return View(tasks);
        }
    }
}
