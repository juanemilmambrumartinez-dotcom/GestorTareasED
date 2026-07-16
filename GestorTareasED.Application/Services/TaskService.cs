using GestorTareasED.Application.Contract;
using GestorTareasED.Application.Dtos.Tasks;
using GestorTareasED.Domain.Entities;
using GestorTareasED.Domain.Repository;

namespace GestorTareasED.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();
        return tasks.Select(t => MapToResponseDto(t));
    }

    public async Task<TaskResponseDto?> GetByIdAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        return task == null ? null : MapToResponseDto(task);
    }

    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto dto)
    {
        await ValidateAsync(dto);

        var task = new TaskItem
        {
            Title = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            Priority = dto.Priority,
            Status = GestorTareasED.Domain.Entities.TaskStatus.Pending,
            DueDate = dto.DueDate,
            ProjectId = dto.ProjectId
        };

        var created = await _taskRepository.CreateAsync(task);

        var project = await _projectRepository.GetByIdAsync(created.ProjectId);
        return MapToResponseDto(created, project?.Name ?? string.Empty);
    }

    public async Task UpdateAsync(int id, CreateTaskDto dto)
    {
        await ValidateAsync(dto);

        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new InvalidOperationException("La tarea especificada no existe.");

        task.Title = dto.Title.Trim();
        task.Description = dto.Description.Trim();
        task.Priority = dto.Priority;
        task.DueDate = dto.DueDate;
        task.ProjectId = dto.ProjectId;

        await _taskRepository.UpdateAsync(task);
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new InvalidOperationException("La tarea especificada no existe.");

        await _taskRepository.DeleteAsync(id);
    }

    private async Task ValidateAsync(CreateTaskDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ArgumentException("El título de la tarea es obligatorio.");

        if (dto.Title.Length > 200)
            throw new ArgumentException("El título no puede superar los 200 caracteres.");

        if (string.IsNullOrWhiteSpace(dto.Description))
            throw new ArgumentException("La descripción de la tarea es obligatoria.");

        if (dto.Description.Length > 1000)
            throw new ArgumentException("La descripción no puede superar los 1000 caracteres.");

        if (dto.DueDate == default)
            throw new ArgumentException("La fecha límite es obligatoria.");

        var projectExists = await _taskRepository.ProjectExistsAsync(dto.ProjectId);
        if (!projectExists)
            throw new ArgumentException("El proyecto especificado no existe.");
    }

    private static TaskResponseDto MapToResponseDto(TaskItem task, string? projectNameOverride = null)
    {
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority.ToString(),
            Status = task.Status.ToString(),
            DueDate = task.DueDate,
            ProjectId = task.ProjectId,
            ProjectName = projectNameOverride ?? (task.Project != null ? task.Project.Name : string.Empty)
        };
    }
}