using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Helpers;
using EntityFrameworkCore.Triggered;

namespace BokAdventure.Persistence.Triggers;
public class AfterSavePlayerTrigger : IAfterSaveTrigger<Player>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AfterSavePlayerTrigger(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;
    public async Task AfterSave(ITriggerContext<Player> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType != ChangeType.Modified) return;

        var player = context.Entity;

        if (player.Experience < player.RequiredExperience) return;

        var upLevelCount = 0;
        var numberOfStatPointEachUpLevel = 2;

        while (player.Experience >= player.RequiredExperience)
        {
            upLevelCount++;
            player.Level++;
            player.Experience -= player.RequiredExperience;
            player.RequiredExperience = ExperienceCalculator.CalculateRequiredExperience(player.Level);
        }

        player.StatPoints += upLevelCount * numberOfStatPointEachUpLevel;

        _applicationDbContext.Update(player);

        await _applicationDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
