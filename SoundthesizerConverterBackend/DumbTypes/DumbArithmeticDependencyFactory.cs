using System;
using System.Collections.Generic;
using System.Diagnostics;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public static class DumbArithmeticDependencyFactory
  {
    public static ITriggerDependency Generate(TriggerDependency dep)
    {
      var result = Generate(dep as Dependency);

      Debug.Assert(result is ITriggerDependency);

      return result as ITriggerDependency;
    }

    public static IDependency Generate(Dependency dep)
    {
      if (!dep.OperatorSpecified)
        throw new SoundthesizerFileFormatException("Cannot create arithmetic dependency without 'operator' attribute.");

      if (dep.Dependencies.Count == 0)
        throw new SoundthesizerFileFormatException(
          "A Dependency with the 'operator' attribute set must contain at least one child Dependency.");

      var children = new List<IDependency>();

      for (int i = 0; i < dep.Dependencies.Count; i++)
      {
        try
        {
          children.Add(DumbDependencyFactory.Generate(dep.Dependencies[i]));
        }
        catch (SoundthesizerFileFormatException e)
        {
          throw new SoundthesizerFileFormatException(String.Format("An error occured while converting Dependency {0}", i), e);
        }
      }

      return (dep is TriggerDependency) ? new DumbArithmeticTriggerDependency(children.AsReadOnly(), dep.Operator, (dep as TriggerDependency).Direction)
                                        : new DumbArithmeticDependency       (children.AsReadOnly(), dep.Operator);
    }
  }
}