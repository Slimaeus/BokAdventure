using System.Reflection;

namespace BokAdventure.Infrastructure;
public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
