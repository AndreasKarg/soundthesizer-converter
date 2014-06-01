using System.Collections.Generic;

namespace SoundthesizerConverterBackend
{
  public interface ISoundSet
  {
    string Name { get; }
    IReadOnlyCollection<ISound> Sounds { get; }
  }

  public class SoundSet : ISoundSet
  {
    private readonly IReadOnlyCollection<ISound> _sounds;

    public string Name { get; private set; }

    public IReadOnlyCollection<ISound> Sounds
    {
      get { return _sounds; }
    }

    public SoundSet(IReadOnlyCollection<ISound> sounds, string name)
    {
      _sounds = sounds;
      Name = name;
    }
  }
}