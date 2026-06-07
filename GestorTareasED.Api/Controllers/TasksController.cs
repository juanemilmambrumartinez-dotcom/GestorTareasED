using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorTareasED.Api.Data;
using GestorTareasED.Api.Models.Entities;
using GestorTareasED.Api.DTOs.Task;

namespace GestorTareasED.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAll()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Project)
                .ToListAsync();

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
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);

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
            var project = await _context.Projects.FindAsync(dto.ProjectId);
            if (project == null) return BadRequest("El proyecto especificado no existe.");

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = Models.Entities.TaskStatus.Pending,
                DueDate = dto.DueDate,
                ProjectId = dto.ProjectId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                ProjectId = task.ProjectId,
                ProjectName = project.Name
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateTaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.ProjectId = dto.ProjectId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}