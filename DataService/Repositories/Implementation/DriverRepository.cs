using DataService.Data;
using Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataService.Repositories.Implementation;

public class DriverRepository: GenericRepository<Driver> , IDriverRepository
{
    public DriverRepository(AppDbContext context, ILogger logger) : base(context, logger)
    { }

    public override async Task<IEnumerable<Driver>> GetAllAsync()
    {
        try
        {
            return await _dbSet.Where(d => d.Status == 1)
                .AsSplitQuery()
                .AsNoTracking()
                .OrderBy(d => d.CreatedAt)
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
        Driver? entity = await _dbSet.FirstOrDefaultAsync(d => d.Id == id);

        if (entity == null)
            return false;


        entity.Status = 0;
        entity.UpdatedAt = DateTime.UtcNow;
        return true;
    }

    public override async Task<bool> UpdateAsync(Driver entity)
    {
        try
        {
            Driver? entityToUpdate = await _dbSet.FirstOrDefaultAsync(d => d.Id == entity.Id);

            if (entityToUpdate == null)
                return false;

            entityToUpdate.UpdatedAt = DateTime.UtcNow;
            entityToUpdate.DriverNumber = entity.DriverNumber;
            entityToUpdate.FirstName = entity.FirstName;
            entityToUpdate.LastName = entity.LastName;
            entityToUpdate.DateOfBirth = entity.DateOfBirth;
            
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "{Repo} UpdateAsync Error" , typeof(DriverRepository));
            throw;
        }
    }
}