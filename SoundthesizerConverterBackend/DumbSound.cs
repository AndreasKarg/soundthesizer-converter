namespace SoundthesizerConverterBackend
{
  public class DumbSound : ISound
  {
    private readonly IDependency _volume;
    private readonly IDependency _pan;
    private readonly IDependency _pitch;
    private readonly ITriggerDependency _trigger;

    public DumbSound(string name, string fileName, IDependency volume, IDependency pan, IDependency pitch, ITriggerDependency trigger)
    {
      FileName = fileName;
      _volume = volume;
      _pan = pan;
      _pitch = pitch;
      _trigger = trigger;
      Name = name;
    }

    public string FileName { get; private set; }
    public string Name { get; private set; }

    public IDependency Volume
    {
      get { return _volume; }
    }

    public IDependency Pan
    {
      get { return _pan; }
    }

    public IDependency Pitch
    {
      get { return _pitch; }
    }

    public ITriggerDependency Trigger
    {
      get { return _trigger; }
    }
  }
}