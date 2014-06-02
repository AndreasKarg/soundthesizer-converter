using System;
using System.Collections.Generic;
using System.Linq;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public static class DumbDependencyFactory
  {
    public static IDependency Generate(Dependency dep)
    {
      if (dep == null)
        return null;

      if (dep.Value == "none")
        return null;

      if (dep.OperatorSpecified && (dep.Value != null))
        throw new SoundthesizerFileFormatException("Operator and Value must not be defined for the same Dependency.");

      //Hack: As the deserializer will always create an empty object even when the respective element is absent in the source file, assume an empty dependency to be absent.
      if (!dep.OperatorSpecified && (dep.Value == null) && (dep.Refpoints.Count == 0) && (dep.Dependencies.Count == 0))
        return null;

      if (!dep.OperatorSpecified && (dep.Value == null))
        throw new SoundthesizerFileFormatException("Either Operator or Value must be defined for a Dependency.");

      return dep.Value != null ? GenerateValueDependency(dep) : GenerateArithmeticDependency(dep);
    }

    public static IDependency GenerateArithmeticDependency(Dependency dep)
    {
      if(!dep.OperatorSpecified)
        throw new SoundthesizerFileFormatException("Cannot create arithmetic dependency without 'operator' attribute.");

      if(dep.Dependencies.Count == 0)
        throw new SoundthesizerFileFormatException("A Dependency with the 'operator' attribute set must contain at least one child Dependency.");

      var children = new List<IDependency>();

      for (int i = 0; i < dep.Dependencies.Count; i++)
      {
        try
        {
          children.Add(Generate(dep.Dependencies[i]));
        }
        catch (SoundthesizerFileFormatException e)
        {
          throw new SoundthesizerFileFormatException(String.Format("An error occured while converting Dependency {0}", i), e);
        }
      }

      return new DumbArithmeticDependency(children.AsReadOnly(), dep.Operator);
    }

    public static IDependency GenerateValueDependency(Dependency dep)
    {
      if(dep.Value == "")
        throw new SoundthesizerFileFormatException("Cannot create value dependency without a value set.");

      if(dep.Refpoints.Count == 0)
        throw new SoundthesizerFileFormatException("A Dependency with the 'value' attribute set must contain at least one refpoint.");

      var refpoints = dep.Refpoints.Select(x => new DoublePoint(x.X, x.Y)).ToList().AsReadOnly();

      //TODO: Actually convert Value type to enum
      return new DumbValueDependency(refpoints, InputType.Bla);
    }

    public static IDependency Generate(Dependency dep, string dependencyName)
    {
      try
      {
        return Generate(dep);
      }
      catch (SoundthesizerFileFormatException e)
      {
        throw new SoundthesizerFileFormatException(string.Format("An error occured while converting property '{0}'", dependencyName), e);
      }
    }
  }
}