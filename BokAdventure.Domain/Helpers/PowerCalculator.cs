using BokAdventure.Domain.Entities;

namespace BokAdventure.Domain.Helpers;

public static class PowerCalculator
{
    public static ulong CalculateHitPoints(ulong baseHP, IEnumerable<PlayerBok> boks)
    {
        var totalHP = baseHP * 500;
        foreach (var playerBok in boks.Where(x => x.Bok != null))
        {
            var bokHP = playerBok.Bok!.HitPoints;
            var amount = playerBok.Amount;
            totalHP += bokHP * amount;
        }
        return baseHP + (ulong)Math.Ceiling(totalHP / (decimal)500);
    }

    public static long CalculateAttack(long baseAtk, IEnumerable<PlayerBok> boks)
    {
        var totalAtk = baseAtk * 25;
        foreach (var playerBok in boks.Where(x => x.Bok != null))
        {
            var bokAtk = playerBok.Bok!.Attack;
            var amount = playerBok.Amount;
            totalAtk += bokAtk * (int)amount;
        }
        return baseAtk + (long)Math.Ceiling(totalAtk / (decimal)25);
    }

    public static long CalculateDefence(long baseDefence, IEnumerable<PlayerBok> boks)
    {
        var totalDefence = baseDefence * 25;
        foreach (var playerBok in boks.Where(x => x.Bok != null))
        {
            var bokDefence = playerBok.Bok!.Defence;
            var amount = playerBok.Amount;
            totalDefence += bokDefence * (int)amount;
        }
        return baseDefence + (long)Math.Ceiling(totalDefence / (decimal)25);
    }

    public static (ulong hp, long atk, long def) CalculateAll(ulong hp, long atk, long def, IEnumerable<PlayerBok> boks)
        => (CalculateHitPoints(hp, boks), CalculateAttack(atk, boks), CalculateDefence(def, boks));
}
