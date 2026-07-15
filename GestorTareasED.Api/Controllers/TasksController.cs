using Microsoft.AspNetCore.Mvc;
using GestorTareasED.Domain.Entities;
using GestorTareasED.Domain.Repository;
using GestorTareasED.Infrastructure.Models;

namespace GestorTareasED.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public TasksController(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAll()
        {
            var tasks = await _taskRepository.GetAllAsync();

            var result = tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority.ToString(),
                Status = t.Status.ToString(),
                DueDate = t.DueDate,
                ProjectId = t.ProjectId,
                ProjectName = t.Project != null ? t.Project.Name : string.Empty
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDto>> GetById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null) return NotFound();

            return Ok(new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                ProjectId = task.ProjectId,
                ProjectName = task.Project != null ? task.Project.Name : string.Empty
            });
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> Create(CreateTaskDto dto)
        {
            var projectExists = await _taskRepository.ProjectExistsAsync(dto.ProjectId);
            if (!projectExists) return BadRequest("El proyecto especificado no existe.");

            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = GestorTareasED.Domain.Entities.TaskStatus.Pending,
                DueDate = dto.DueDate,
                ProjectId = dto.ProjectId
            };

            var created = await _taskRepository.CreateAsync(task);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new TaskResponseDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                Priority = created.Priority.ToString(),
                Status = created.Status.ToString(),
                DueDate = created.DueDate,
                ProjectId = created.ProjectId,
                ProjectName = project != null ? project.Name : string.Empty
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateTaskDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.ProjectId = dto.ProjectId;

            await _taskRepository.UpdateAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return NotFound();

            await _taskRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}