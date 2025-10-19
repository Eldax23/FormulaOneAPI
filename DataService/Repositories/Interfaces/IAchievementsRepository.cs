using Entites;

namespace DataService.Repositories;

public interface IAchievementsRepository : IGenericRepository<Achievement>
{
    Task<Achievement?> GetAchievementById(Guid id);
    Task<IEnumerable<Achievement>> GetAchievemntsForDriverAsync(Guid driverId);
}