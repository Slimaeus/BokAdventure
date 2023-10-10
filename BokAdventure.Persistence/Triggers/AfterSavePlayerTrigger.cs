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

        //if (context.UnmodifiedEntity is null) return;

        var player = context.Entity;
        //var oldPlayer = context.UnmodifiedEntity;

        if (player.Experience < player.RequiredExperience) return;

        while (player.Experience >= player.RequiredExperience)
        {
            player.Level++;
            player.RequiredExperience = ExperienceCalculator.CalculateRequiredExperience(player.Level);
        }

        _applicationDbContext.Update(player);

        await _applicationDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
