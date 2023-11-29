using Microsoft.EntityFrameworkCore;
using ToDo.Data;

namespace ToDo.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ToDoContext(serviceProvider.GetRequiredService<
                DbContextOptions<ToDoContext>>()))
            {
                if (context.Tasks.Any())
                    return;
                else 
                {
                    context.Tasks.AddRange(
                    new Tasks("Сomplete TT", Theme.ThemeType.Work, Status.StatusType.Done, "Сomplete the technical task for the ASP.NET developer position"),
                    new Tasks("Interview", Theme.ThemeType.Work, Status.StatusType.InProgress, "Get an interview for the ASP.NET developer position"),
                    new Tasks("Start working", Theme.ThemeType.Work, Status.StatusType.InProgress, "Start working on an interesting product")
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
