using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend
{
  public static class DumbSoundsetFactory
  {
    public static IDependency GenerateDependencyFromFile(Dependency dep)
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

      return dep.value != null ? GenerateValueDependencyFromFile(dep) : GenerateArithmeticDependencyFromFile(dep);
    }

    private static IDependency GenerateArithmeticDependencyFromFile(Dependency dep)
    {
      if(dep.dependency.Count == 0)
        throw new SoundthesizerFileFormatException("A Dependency with the 'operator' attribute set must contain at least one child Dependency.");

      var children = new List<IDependency>();

      for (int i = 0; i < dep.dependency.Count; i++)
      {
        try
        {
          children.Add(GenerateDependencyFromFile(dep.dependency[i]));
        }
        catch (SoundthesizerFileFormatException e)
        {
          throw new SoundthesizerFileFormatException(String.Format("An error occured while converting Dependency {0}", i), e);
        }
      }

      //TODO: Actually convert the operator
      return new DumbArithmeticDependency(children.AsReadOnly(), Operator.Add);
    }

    private static IDependency GenerateValueDependencyFromFile(Dependency dep)
    {
      if(dep.refpoint.Count == 0)
        throw new SoundthesizerFileFormatException("A Dependency with the 'value' attribute set must contain at least one refpoint.");

      var refpoints = dep.refpoint.Select(x => new DoublePoint(x.x, x.y)).ToList().AsReadOnly();

      //TODO: Actually convert Value type to enum
      return new DumbValueDependency(refpoints, InputType.Bla);
    }

    private static IDependency GenerateDependencyFromFileWithExceptionDecoration(Dependency dep, string soundProperty)
    {
      try
      {
        return GenerateDependencyFromFile(dep);
      }
      catch (SoundthesizerFileFormatException e)
      {
        throw new SoundthesizerFileFormatException(string.Format("An error occured while converting property '{0}'", soundProperty), e);
      }
    }

    public static ISound GenerateSoundFromFile(soundsetSound sound)
    {
      IDependency volume, pan, pitch;
      //TODO: Rework Trigger Dependency handling via decorator
      IDependency trigger;

      try
      {

        volume = GenerateDependencyFromFileWithExceptionDecoration(sound.volume, "volume");
        pan = GenerateDependencyFromFileWithExceptionDecoration(sound.pan, "pan");
        pitch = GenerateDependencyFromFileWithExceptionDecoration(sound.frequency, "frequency");

        trigger = GenerateDependencyFromFileWithExceptionDecoration(sound.trigger, "trigger");
      }
      catch (SoundthesizerFileFormatException e)
      {
        throw new SoundthesizerFileFormatException(string.Format("An error occured while converting sound '{0}'", sound.name), e);
      }

      //TODO: Pass on trigger properly!
      return new DumbSound(sound.name, sound.file.name, volume, pan, pitch, null);
    }

    public static SoundSet GenerateSoundsetFromFile(string filename)
    {
      var soundSet = soundset.LoadFromFile(filename);

      var sounds = soundSet.Sounds.Select(GenerateSoundFromFile).ToList().AsReadOnly();

      return new SoundSet(sounds, soundSet.name);
    }
  }

  public class SoundthesizerFileFormatException : Exception
  {
    public SoundthesizerFileFormatException(string message = "", Exception innerException = null) : base(message, innerException)
    {
    }
  }
}