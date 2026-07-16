using GestorTareasED.Application.Dtos.Tasks;

namespace GestorTareasED.Application.Contract;

public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllAsync();
    Task<TaskResponseDto?> GetByIdAsync(int id);
    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto);
    Task UpdateAsync(int id, CreateTaskDto dto);
    Task DeleteAsync(int id);
}