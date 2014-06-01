namespace SoundthesizerConverterBackend
{
  public interface ISound
  {
    string FileName { get; }
    string Name { get; }
    IDependency Volume { get; }
    IDependency Pan { get; }
    IDependency Pitch { get; }
    ITriggerDependency Trigger { get; }
  }
}