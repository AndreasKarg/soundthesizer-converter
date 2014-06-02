using System.Collections.Generic;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public class DumbValueDependency : IValueDependency
  {
    private readonly IReadOnlyCollection<DoublePoint> _refPoints;

    public DumbValueDependency(IReadOnlyCollection<DoublePoint> refPoints, InputType inputType)
    {
      InputType = inputType;
      _refPoints = refPoints;
    }

    public IReadOnlyCollection<DoublePoint> RefPoints
    {
      get { return _refPoints; }
    }

    public InputType InputType { get; private set; }
  }
}