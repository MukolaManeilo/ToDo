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
            tasks = tasks.OrderBy(tasks => tasks.Status).ToList();
            return View(tasks);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] string? sorting, [FromForm] string? status, [FromForm] string? theme)
        {
            List<Tasks> tasks = _context.Tasks.ToList();
            //Filter
            if (status != null)
            {
                if(status == "InProgress")
                    tasks = _context.Tasks.Where(tasks => tasks.Status == Status.StatusType.InProgress).ToList();
                else if(status == "Done")
                    tasks = _context.Tasks.Where(tasks => tasks.Status == Status.StatusType.Done).ToList();
            }
            if (theme != null)
            {
                if (theme == "None")
                    tasks = tasks.Where(tasks => tasks.Theme == Theme.ThemeType.None).ToList();
                else if (theme == "Work")
                    tasks = tasks.Where(tasks => tasks.Theme == Theme.ThemeType.Work).ToList();
                else if (theme == "Study")
                    tasks = tasks.Where(tasks => tasks.Theme == Theme.ThemeType.Study).ToList();
                else if (theme == "Housework")
                    tasks = tasks.Where(tasks => tasks.Theme == Theme.ThemeType.Housework).ToList();
                else if (theme == "Other")
                    tasks = tasks.Where(tasks => tasks.Theme == Theme.ThemeType.Other).ToList();
            }

            //Default sort
            tasks = tasks.OrderBy(tasks => tasks.Status).ToList();

            //Sort
            if (sorting != null)
            {
                if (sorting == "DateAdded")
                    tasks = tasks.OrderByDescending(tasks => tasks.CreatedAt).ToList();
                else if (sorting == "Alphabet") 
                    tasks = tasks.OrderBy(tasks => tasks.Title, StringComparer.OrdinalIgnoreCase).ToList();
            }
            return View(tasks);
        }

    }
}

