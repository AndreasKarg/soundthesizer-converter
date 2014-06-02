using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public static class DumbValueDependencyFactory
  {
    public static ITriggerDependency Generate(TriggerDependency dep)
    {
      var result = Generate(dep as Dependency);

      Debug.Assert(result is ITriggerDependency);
      return result as ITriggerDependency;
    }

    public static IDependency Generate(Dependency dep)
    {
      if(dep.Value == "")
        throw new SoundthesizerFileFormatException("Cannot create value dependency without a value set.");

      if(dep.Refpoints.Count == 0)
        throw new SoundthesizerFileFormatException("A Dependency with the 'value' attribute set must contain at least one refpoint.");

      var refpoints = dep.Refpoints.Select(x => new DoublePoint(x.X, x.Y)).ToList().AsReadOnly();

      //TODO: Actually convert Value type to enum
      return (dep is TriggerDependency) ? new DumbValueTriggerDependency(refpoints, InputType.Bla, (dep as TriggerDependency).Direction) : new DumbValueDependency(refpoints, InputType.Bla);
    }
  }
}