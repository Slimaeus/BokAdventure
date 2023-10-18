namespace BokAdventure.Domain.Helpers;
public static class ExperienceCalculator
{
    // Constants for experience calculation
    private const double _baseExperience = 100; // Base experience required for level 1
    private const double _experienceGrowthFactor = 1.2; // Experience required multiplier for each level increase

    public static ulong CalculateRequiredExperience(ulong currentLevel)
    {
        // Calculate required experience for the next level using exponential growth formula
        double requiredExperience = _baseExperience * Math.Pow(_experienceGrowthFactor, currentLevel - 1);

        // Round up the required experience to the nearest integer
        return (ulong)Math.Ceiling(requiredExperience);
    }
}
