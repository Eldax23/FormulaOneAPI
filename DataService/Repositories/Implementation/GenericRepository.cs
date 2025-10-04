using DataService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataService.Repositories.Implementation;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly ILogger _logger;
    internal DbSet<T> _dbSet;
    public GenericRepository(AppDbContext context , ILogger logger)
    {
        _context = context;
        _logger = logger;
        _dbSet = _context.Set<T>();
    }
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> AddAsync(T entity)
    {
        _context.Add(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public virtual Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}