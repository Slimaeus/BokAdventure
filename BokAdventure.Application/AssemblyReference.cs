using System.Reflection;

namespace BokAdventure.Application;
public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
