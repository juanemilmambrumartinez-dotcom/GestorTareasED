using Microsoft.EntityFrameworkCore;
using GestorTareasED.Domain.Entities;
using GestorTareasED.Domain.Repository;
using GestorTareasED.Infrastructure.Context;
using GestorTareasED.Infrastructure.Core;

namespace GestorTareasED.Infrastructure.Repositories;

public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks.Include(t => t.Project).ToListAsync();
    }

    public override async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks.Include(t => t.Project)
                                    .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> ProjectExistsAsync(int projectId)
    {
        return await _context.Projects.AnyAsync(p => p.Id == projectId);
    }
}