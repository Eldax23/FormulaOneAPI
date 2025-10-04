using DataService.Data;
using Microsoft.Extensions.Logging;

namespace DataService.Repositories.Implementation;

public class UnitOfWork : IUnitOfWork , IDisposable
{
    private readonly AppDbContext _context;
    public IDriverRepository Drivers { get; }
    public IAchievementsRepository Achievements { get; }

    public UnitOfWork(AppDbContext context , ILoggerFactory loggerFactory)
    {
        _context = context;
        var logger = loggerFactory.CreateLogger("logs");

        Drivers = new DriverRepository(_context , logger);
        Achievements = new AchievementRepository(_context , logger);
    }
    
    
    
    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}