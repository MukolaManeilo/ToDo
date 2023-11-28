using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ToDo.Models
{
    public class Tasks
    {
        public Tasks(string Title, string Theme, string Status, string Content)
        {
            this.Title = Title;
            this.Theme = Theme;
            this.Status = Status;
            CreatedAt = DateTime.UtcNow;
            this.Content = Content;
        }

        public Tasks(int Id, string Title, string Theme, string Status, DateTime? CreatedAt, string Content)
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
        [MaxLength(200)]
        [MinLength(3)]
        public string Theme { get; set; }


        [Required]
        [Column("status")]
        public string Status { get; set; }


        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }


        [Required]
        [Column("content")]
        [MaxLength(200)]
        [MinLength(3)]
        public string Content { get; set; }
    }
}
