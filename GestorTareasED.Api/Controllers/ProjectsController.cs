using Microsoft.AspNetCore.Mvc;
using GestorTareasED.Domain.Entities;
using GestorTareasED.Domain.Repository;
using GestorTareasED.Infrastructure.Models;

namespace GestorTareasED.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetAll()
        {
            var projects = await _projectRepository.GetAllAsync();
            var result = projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                IsActive = p.IsActive
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponseDto>> GetById(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return NotFound();

            return Ok(new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                IsActive = project.IsActive
            });
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponseDto>> Create(CreateProjectDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = true
            };

            var created = await _projectRepository.CreateAsync(project);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new ProjectResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                StartDate = created.StartDate,
                EndDate = created.EndDate,
                IsActive = created.IsActive
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateProjectDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return NotFound();

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;

            await _projectRepository.UpdateAsync(project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return NotFound();

            await _projectRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}