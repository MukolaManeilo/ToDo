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
                {
                    return;
                }
                else 
                {
                    context.Tasks.AddRange(
                    new Tasks("Сomplete technical task", "Work", "InProgress", "Сomplete the technical task for the ASP.NET developer position and pass the interview")
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
