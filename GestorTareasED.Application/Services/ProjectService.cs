using GestorTareasED.Application.Contract;
using GestorTareasED.Application.Dtos.Project;
using GestorTareasED.Domain.Entities;
using GestorTareasED.Domain.Repository;

namespace GestorTareasED.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        return projects.Select(MapToResponseDto);
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        return project == null ? null : MapToResponseDto(project);
    }

    public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto)
    {
        Validate(dto);

        var project = new Project
        {
            Name = dto.Name.Trim(),
            Description = dto.Description.Trim(),
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsActive = true
        };

        var created = await _projectRepository.CreateAsync(project);
        return MapToResponseDto(created);
    }

    public async Task UpdateAsync(int id, CreateProjectDto dto)
    {
        Validate(dto);

        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null)
            throw new InvalidOperationException("El proyecto especificado no existe.");

        project.Name = dto.Name.Trim();
        project.Description = dto.Description.Trim();
        project.StartDate = dto.StartDate;
        project.EndDate = dto.EndDate;

        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteAsync(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null)
            throw new InvalidOperationException("El proyecto especificado no existe.");

        await _projectRepository.DeleteAsync(id);
    }

    private static void Validate(CreateProjectDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("El nombre del proyecto es obligatorio.");

        if (dto.Name.Length > 200)
            throw new ArgumentException("El nombre del proyecto no puede superar los 200 caracteres.");

        if (string.IsNullOrWhiteSpace(dto.Description))
            throw new ArgumentException("La descripción del proyecto es obligatoria.");

        if (dto.Description.Length > 1000)
            throw new ArgumentException("La descripción no puede superar los 1000 caracteres.");

        if (dto.StartDate == default)
            throw new ArgumentException("La fecha de inicio es obligatoria.");

        if (dto.EndDate == default)
            throw new ArgumentException("La fecha de fin es obligatoria.");

        if (dto.EndDate <= dto.StartDate)
            throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio.");
    }

    private static ProjectResponseDto MapToResponseDto(Project project)
    {
        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            IsActive = project.IsActive
        };
    }
}