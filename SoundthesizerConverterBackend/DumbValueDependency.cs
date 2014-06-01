using System.Collections.Generic;
using System.Drawing;

namespace SoundthesizerConverterBackend
{
  public class DumbValueDependency : IValueDependency, ITriggerDependency
  {
    private readonly IReadOnlyCollection<DoublePoint> _refPoints;

    public DumbValueDependency(IReadOnlyCollection<DoublePoint> refPoints, InputType inputType, TriggerDirection direction = TriggerDirection.Both)
    {
      InputType = inputType;
      _refPoints = refPoints;
      Direction = direction;
    }

    public IReadOnlyCollection<DoublePoint> RefPoints
    {
      get { return _refPoints; }
    }

    public InputType InputType { get; private set; }
    public TriggerDirection Direction { get; private set; }
  }
}