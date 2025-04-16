using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task_dotnet_app.Data.Model
{
    public class TaskItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; } 

        [Required]
        public string TaskName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
