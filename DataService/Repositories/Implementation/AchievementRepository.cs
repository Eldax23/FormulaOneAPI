using DataService.Data;
using Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataService.Repositories.Implementation;

public class AchievementRepository : GenericRepository<Achievement> , IAchievementsRepository
{
    public AchievementRepository(AppDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public async Task<Achievement?> GetAchievementById(Guid id)
    {
        try
        {
            Achievement? result = await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message , "{Repo} GetAchievement function Error" , typeof(AchievementRepository));
            throw;
        }
    }
    
    public override async Task<IEnumerable<Achievement>> GetAllAsync()
    {
        try
        {
            return await _dbSet.Where(a => a.Status == 1)
                .AsSplitQuery()
                .AsNoTracking()
                .OrderBy(a => a.CreatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message , "{Repo} All function Error" , typeof(DriverRepository));
            throw;
        }
    }

    public override async Task<bool> DeleteAsync(Guid id)
    {
        // find the entity
        Achievement? entity = await _dbSet.FirstOrDefaultAsync(a => a.Id == id);

        if (entity == null)
            return false;


        entity.Status = 0;
        entity.UpdatedAt = DateTime.UtcNow;
        return true;
    }

    public async Task<IEnumerable<Achievement>> GetAchievemntsForDriverAsync(Guid driverId)
    {
        try
        {
            return await _dbSet.Where(a => a.DriverId == driverId && a.Status == 1)
                .AsNoTracking()
                .OrderBy(a => a.CreatedAt)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
    }

    public override async Task<bool> UpdateAsync(Achievement entity)
    {
        try
        {
            Achievement? entityToUpdate = await _dbSet.FirstOrDefaultAsync(d => d.Id == entity.Id);

            if (entityToUpdate == null)
                return false;

            entityToUpdate.UpdatedAt = DateTime.UtcNow;
            entityToUpdate.FastestLap = entity.FastestLap;
            entityToUpdate.PolePosition = entity.PolePosition;
            entityToUpdate.RaceWins = entity.RaceWins;
            entityToUpdate.WorldChampionship = entity.WorldChampionship;
            
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "{Repo} UpdateAsync Error" , typeof(DriverRepository));
            throw;
        }
    }
}