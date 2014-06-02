using System;
using System.Linq;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public static class DumbSoundsetFactory
  {
    public static DumbSoundSet GenerateFromSoundthesizer(string filename)
    {
      var soundSet = soundset.LoadFromFile(filename);

      var sounds = soundSet.Sounds.Select(DumbSoundFactory.GenerateFromSoundthesizer).ToList().AsReadOnly();

      return new DumbSoundSet(sounds, soundSet.name);
    }
  }

  public class SoundthesizerFileFormatException : Exception
  {
    public SoundthesizerFileFormatException(string message = "", Exception innerException = null) : base(message, innerException)
    {
    }
  }
}