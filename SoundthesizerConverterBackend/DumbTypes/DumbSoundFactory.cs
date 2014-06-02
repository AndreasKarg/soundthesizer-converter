using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public static class DumbSoundFactory
  {
    public static ISound GenerateFromSoundthesizer(Sound sound)
    {
      IDependency volume, pan, pitch;
      //TODO: Rework Trigger Dependency handling via decorator
      IDependency trigger;

      try
      {

        volume = DumbDependencyFactory.Generate(sound.Volume, "volume");
        pan = DumbDependencyFactory.Generate(sound.Pan, "pan");
        pitch = DumbDependencyFactory.Generate(sound.Frequency, "frequency");

        trigger = DumbDependencyFactory.Generate(sound.Trigger, "trigger");
      }
      catch (SoundthesizerFileFormatException e)
      {
        throw new SoundthesizerFileFormatException(string.Format("An error occured while converting sound '{0}'", sound.Name), e);
      }

      //TODO: Pass on trigger properly!
      return new DumbSound(sound.Name, sound.File.Name, volume, pan, pitch, null);
    }
  }
}