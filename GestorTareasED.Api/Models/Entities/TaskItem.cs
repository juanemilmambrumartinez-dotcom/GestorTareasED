using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorTareasED.Api.Models.Entities
{
    public enum TaskPriority { High, Medium, Low }
    public enum TaskStatus { Pending, InProgress, Completed }

    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        public DateTime DueDate { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}