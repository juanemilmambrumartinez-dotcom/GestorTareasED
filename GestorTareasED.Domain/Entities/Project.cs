using GestorTareasED.Domain.Core;

namespace GestorTareasED.Domain.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    
    public Project()
    {
    }

    
    public Project(string name, string description, DateTime startDate, DateTime endDate)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = true;
    }

   
    public Project(string name, string description, DateTime startDate, DateTime endDate, bool isActive)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = isActive;
    }
}