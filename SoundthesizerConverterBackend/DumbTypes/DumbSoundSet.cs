using System.Collections.Generic;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public class DumbSoundSet : ISoundSet
  {
    private readonly IReadOnlyCollection<ISound> _sounds;

    public string Name { get; private set; }

    public IReadOnlyCollection<ISound> Sounds
    {
      get { return _sounds; }
    }

    public DumbSoundSet(IReadOnlyCollection<ISound> sounds, string name)
    {
      _sounds = sounds;
      Name = name;
    }
  }
}