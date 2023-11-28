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
                    new Tasks("Сomplete TT", Theme.ThemeType.Work, Status.StatusType.Done, "Сomplete the technical task for the ASP.NET developer position and pass the interview"),
                    new Tasks("Example done task", Theme.ThemeType.Study, Status.StatusType.InProgress, "test text, test text, test text, test text"),
                    new Tasks("Example inProgress task", Theme.ThemeType.None, Status.StatusType.Done, "test text, test text, test text, test text, test text, test text, test text, test text, test text, test text, test text, test text")
                    
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
