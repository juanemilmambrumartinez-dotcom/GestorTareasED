using GestorTareasED.Domain.Entities;

namespace GestorTareasED.Domain.Repository;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(int id);
    Task<bool> ProjectExistsAsync(int projectId);
}