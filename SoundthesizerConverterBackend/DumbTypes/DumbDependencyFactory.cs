using System;
using System.Collections.Generic;
using System.Linq;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public static class DumbDependencyFactory
  {
    public static IDependency GenerateFromSoundthesizer(Dependency dep)
    {
      if (dep == null)
        return null;

      if (dep.value == "none")
        return null;

      if (dep.operatorSpecified && (dep.value != null))
        throw new SoundthesizerFileFormatException("Operator and Value must not be defined for the same Dependency.");

      //Hack: As the deserializer will always create an empty object even when the respective element is absent in the source file, assume an empty dependency to be absent.
      if (!dep.operatorSpecified && (dep.value == null) && (dep.refpoint.Count == 0) && (dep.dependency.Count == 0))
        return null;

      if (!dep.operatorSpecified && (dep.value == null))
        throw new SoundthesizerFileFormatException("Either Operator or Value must be defined for a Dependency.");

      return dep.value != null ? GenerateValueTypeFromSoundthesizer(dep) : GenerateArithmeticTypeFromSoundthesizer(dep);
    }

    private static IDependency GenerateArithmeticTypeFromSoundthesizer(Dependency dep)
    {
      if(dep.dependency.Count == 0)
        throw new SoundthesizerFileFormatException("A Dependency with the 'operator' attribute set must contain at least one child Dependency.");

      var children = new List<IDependency>();

      for (int i = 0; i < dep.dependency.Count; i++)
      {
        try
        {
          children.Add(GenerateFromSoundthesizer(dep.dependency[i]));
        }
        catch (SoundthesizerFileFormatException e)
        {
          throw new SoundthesizerFileFormatException(String.Format("An error occured while converting Dependency {0}", i), e);
        }
      }

      //TODO: Actually convert the operator
      return new DumbArithmeticDependency(children.AsReadOnly(), Operator.Add);
    }

    private static IDependency GenerateValueTypeFromSoundthesizer(Dependency dep)
    {
      if(dep.refpoint.Count == 0)
        throw new SoundthesizerFileFormatException("A Dependency with the 'value' attribute set must contain at least one refpoint.");

      var refpoints = dep.refpoint.Select(x => new DoublePoint(x.x, x.y)).ToList().AsReadOnly();

      //TODO: Actually convert Value type to enum
      return new DumbValueDependency(refpoints, InputType.Bla);
    }

    public static IDependency GenerateFromFile(Dependency dep, string dependencyName)
    {
      try
      {
        return GenerateFromSoundthesizer(dep);
      }
      catch (SoundthesizerFileFormatException e)
      {
        throw new SoundthesizerFileFormatException(string.Format("An error occured while converting property '{0}'", dependencyName), e);
      }
    }
  }
}