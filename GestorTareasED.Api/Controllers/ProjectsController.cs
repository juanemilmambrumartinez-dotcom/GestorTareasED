using Microsoft.AspNetCore.Mvc;
using GestorTareasED.Api.Models.Entities;

namespace GestorTareasED.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private static List<Project> _projects = new List<Project>();
        private static List<TaskItem> _tasks = new List<TaskItem>();
        private static int _nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetAll()
        {
            return Ok(_projects);
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetById(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpGet("{id}/tasks")]
        public ActionResult<IEnumerable<TaskItem>> GetTasks(int id)
        {
            var tasks = _tasks.Where(t => t.ProjectId == id).ToList();
            return Ok(tasks);
        }

        [HttpPost]
        public ActionResult<Project> Create(Project project)
        {
            project.Id = _nextId++;
            _projects.Add(project);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Project updated)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();
            project.Name = updated.Name;
            project.Description = updated.Description;
            project.StartDate = updated.StartDate;
            project.EndDate = updated.EndDate;
            project.IsActive = updated.IsActive;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();
            _projects.Remove(project);
            return NoContent();
        }
    }
}