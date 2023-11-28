using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
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

        public async Task<IActionResult> Index()
        {
            return _context.Tasks != null ?
                        View(await _context.Tasks.ToListAsync()) :
                        Problem("Entity set 'ASPNETCoreContext.Tasks'  is null.");
        }

        [HttpGet]
        public IActionResult Create(int? taskId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {

            Tasks tasks = new(
                HttpContext.Request.Form["Title"],
                (Theme.ThemeType)Enum.Parse(typeof(Theme.ThemeType), HttpContext.Request.Form["Theme"]),
                Status.StatusType.InProgress,
                HttpContext.Request.Form["Content"]
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
        public IActionResult Edit(int taskId)
        {
            Tasks tasks = _context.Tasks.First(user => user.Id == taskId);
            return View(tasks);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit()
        {
            Tasks tasks = new(
                int.Parse(HttpContext.Request.Form["Id"]),
                HttpContext.Request.Form["Title"],
                (Theme.ThemeType)Enum.Parse(typeof(Theme.ThemeType), HttpContext.Request.Form["Theme"]),
                (Status.StatusType)Enum.Parse(typeof(Status.StatusType), HttpContext.Request.Form["Status"]),
                DateTime.Parse(HttpContext.Request.Form["CreatedAt"]),
                HttpContext.Request.Form["Content"]
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
