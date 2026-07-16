using GestorTareasED.Domain.Entities;

namespace GestorTareasED.Application.Dtos.Tasks;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime DueDate { get; set; }
    public int ProjectId { get; set; }
}