using Entites;

namespace DataService.Repositories;

public interface IAchievementsRepository : IGenericRepository<Achievement>
{
    Task<Achievement?> GetDriverAchievementByIdAsync(Guid id);
}