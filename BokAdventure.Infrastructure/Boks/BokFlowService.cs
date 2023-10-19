using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Enumerations;
using BokAdventure.Domain.Interfaces;
using BokAdventure.Persistence;

namespace BokAdventure.Infrastructure.Boks;

public sealed class BokFlowService : IBokFlowService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public BokFlowService(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;
    public async Task<Bok> Get(BokIdentify id)
    {
        var bok = await _applicationDbContext.Boks.FindAsync(id)
            ?? throw new Exception("Bok not found");
        return bok;
    }
}
