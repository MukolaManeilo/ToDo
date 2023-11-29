using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class TasksController : Controller
    {
        private readonly ToDoContext _context;

        public TasksController(ToDoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Create(int? taskId)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] string Title, [FromForm] string Theme, [FromForm] string Content)
        {
            if (Title == null || Theme == null || Content == null)
                return RedirectToAction("Create");

            Theme.ThemeType defaultThemeType = Models.Theme.ThemeType.Other;
            //Validity check
            if (Enum.TryParse(Theme, out Theme.ThemeType theme))
                theme = (Theme.ThemeType)Enum.Parse(typeof(Theme.ThemeType), Theme);
            else
                theme = defaultThemeType;

            Tasks tasks = new(
                Title,
                theme,
                Status.StatusType.InProgress,
                Content
                );

            if (ModelState.IsValid)
            {
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(tasks);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int taskId)
        {
            Tasks tasks =  await _context.Tasks.FindAsync(taskId);
            return View(tasks);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] string Id, [FromForm] string Title, [FromForm] string? Theme, [FromForm] string Status, [FromForm] string CreatedAt, [FromForm] string Content)
        {
            if (Theme == null)
                return RedirectToAction("Edit");

            Theme.ThemeType defaultThemeType = Models.Theme.ThemeType.Other;
            //Validity check
            if (Enum.TryParse(Theme, out Theme.ThemeType theme))
                theme = (Theme.ThemeType)Enum.Parse(typeof(Theme.ThemeType), Theme);
            else
                theme = defaultThemeType;

            Tasks tasks = new(
                int.Parse(Id),
                Title,
                theme,
                (Status.StatusType)Enum.Parse(typeof(Status.StatusType), Status),
                DateTime.Parse(CreatedAt),
                Content
                );

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index","Home");
            }
            return View(tasks);
        }


        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int taskId, string newStatus)
        {
            Tasks tasks = await _context.Tasks.FindAsync(taskId);
            tasks.Status = (Status.StatusType)Enum.Parse(typeof(Status.StatusType), newStatus) ;

            if (tasks != null)
                _context.Tasks.Update(tasks);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int taskId)
        {
            var tasks = await _context.Tasks.FindAsync(taskId);

            if (tasks != null)
                _context.Tasks.Remove(tasks);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
