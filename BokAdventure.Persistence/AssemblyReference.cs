using System.Reflection;

namespace BokAdventure.Persistence;
public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
