using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Helpers;
using EntityFrameworkCore.Triggered;

namespace BokAdventure.Persistence.Triggers;
public class AfterLevelUpTrigger : IAfterSaveTrigger<Player>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AfterLevelUpTrigger(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;
    public async Task AfterSave(ITriggerContext<Player> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType != ChangeType.Modified) return;

        if (context.UnmodifiedEntity is null) return;

        if (context.Entity.Level <= context.UnmodifiedEntity.Level) return;

        var player = context.Entity;

        player.RequiredExperience = ExperienceCalculator.CalculateRequiredExperience(player.Level);

        _applicationDbContext.Update(player);

        await _applicationDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
