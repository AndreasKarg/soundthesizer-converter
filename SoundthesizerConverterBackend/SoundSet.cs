using System.Collections.Generic;

namespace SoundthesizerConverterBackend
{
  public interface ISoundSet
  {
    string Name { get; }
    IReadOnlyCollection<ISound> Sounds { get; }
  }
}