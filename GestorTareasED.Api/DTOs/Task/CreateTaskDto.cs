using GestorTareasED.Api.Models.Entities;

namespace GestorTareasED.Api.DTOs.Task
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
    }
}