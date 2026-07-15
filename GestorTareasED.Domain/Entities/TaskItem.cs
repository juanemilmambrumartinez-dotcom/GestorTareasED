using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestorTareasED.Domain.Core;

namespace GestorTareasED.Domain.Entities;

public enum TaskPriority
{
    High,
    Medium,
    Low
}

public enum TaskStatus
{
    Pending,
    InProgress,
    Completed
}

public class TaskItem : BaseEntity
{
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