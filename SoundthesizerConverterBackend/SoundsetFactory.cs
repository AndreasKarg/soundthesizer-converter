using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend
{
  public static class DumbSoundsetFactory
  {
    public static SoundSet GenerateFromSoundthesizer(string filename)
    {
      var soundSet = soundset.LoadFromFile(filename);

      var sounds = soundSet.Sounds.Select(DumbSoundFactory.GenerateFromSoundthesizer).ToList().AsReadOnly();

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