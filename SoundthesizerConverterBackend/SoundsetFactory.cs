using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend
{
  public static class DumbSoundsetFactory
  {
    public static ISound GenerateSoundFromFile(soundsetSound sound)
    {
      IDependency volume, pan, pitch;
      //TODO: Rework Trigger Dependency handling via decorator
      IDependency trigger;

      try
      {

        volume = DumbDependencyFactory.GenerateFromFile(sound.volume, "volume");
        pan = DumbDependencyFactory.GenerateFromFile(sound.pan, "pan");
        pitch = DumbDependencyFactory.GenerateFromFile(sound.frequency, "frequency");

        trigger = DumbDependencyFactory.GenerateFromFile(sound.trigger, "trigger");
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