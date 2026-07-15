using GestorTareasED.Domain.Entities;
using GestorTareasED.Domain.Repository;
using GestorTareasED.Infrastructure.Context;
using GestorTareasED.Infrastructure.Core;

namespace GestorTareasED.Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(AppDbContext context) : base(context)
    {
    }
}