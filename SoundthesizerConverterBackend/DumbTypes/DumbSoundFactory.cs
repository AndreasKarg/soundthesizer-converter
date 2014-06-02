using System;
using System.Diagnostics;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public static class DumbSoundFactory
  {
    public static ISound GenerateFromSoundthesizer(Sound sound)
    {
      IDependency volume, pan, pitch;
      ITriggerDependency trigger;

      try
      {

        volume = GenerateDependency(sound.Volume, "volume");
        pan = GenerateDependency(sound.Pan, "pan");
        pitch = GenerateDependency(sound.Frequency, "frequency");

        trigger = GenerateDependency(sound.Trigger);
      }
      catch (SoundthesizerFileFormatException e)
      {
        throw new SoundthesizerFileFormatException(String.Format("An error occured while converting sound '{0}'", sound.Name), e);
      }

      return new DumbSound(sound.Name, sound.File.Name, volume, pan, pitch, trigger);
    }

    private static IDependency GenerateDependency(Dependency dep, string dependencyName)
    {
      try
      {
        return DumbDependencyFactory.Generate(dep);
      }
      catch (SoundthesizerFileFormatException e)
      {
        throw new SoundthesizerFileFormatException(String.Format("An error occured while converting property '{0}'", dependencyName), e);
      }
    }

    private static ITriggerDependency GenerateDependency(TriggerDependency dep)
    {
      var result = GenerateDependency(dep, "trigger");

      Debug.Assert(result is ITriggerDependency);

      return result as ITriggerDependency;
    }
  }
}