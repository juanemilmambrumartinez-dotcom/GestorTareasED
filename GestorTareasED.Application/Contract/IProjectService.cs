using GestorTareasED.Application.Dtos.Project;

namespace GestorTareasED.Application.Contract;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto);
    Task UpdateAsync(int id, CreateProjectDto dto);
    Task DeleteAsync(int id);
}