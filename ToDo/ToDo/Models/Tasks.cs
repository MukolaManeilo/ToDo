using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ToDo.Models
{
    public class Tasks
    {
        public Tasks(string Title, Theme.ThemeType Theme, Status.StatusType Status, string Content)
        {
            this.Title = Title;
            this.Theme = Theme;
            this.Status = Status;
            CreatedAt = DateTime.UtcNow;
            this.Content = Content;
        }

        public Tasks(int Id, string Title, Theme.ThemeType Theme, Status.StatusType Status, DateTime? CreatedAt, string Content)
        {
            this.Id = Id;
            this.Title = Title;
            this.Theme = Theme;
            this.Status = Status;
            this.CreatedAt = CreatedAt;
            this.Content = Content;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [Column("title")]
        [MaxLength(30)]
        [MinLength(3)]
        public string Title { get; set; }


        [Required]
        [Column("theme")]
        public Theme.ThemeType Theme { get; set; }


        [Required]
        [Column("status")]
        public Status.StatusType Status { get; set; }


        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }


        [Required]
        [Column("content")]
        [MaxLength(200)]
        [MinLength(3)]
        public string Content { get; set; }
    }
}
