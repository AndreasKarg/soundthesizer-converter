using System.Collections.Generic;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public class DumbValueTriggerDependency : DumbValueDependency, ITriggerDependency
  {
    public DumbValueTriggerDependency(IReadOnlyCollection<DoublePoint> refPoints, InputType inputType, TriggerDirection direction) : base(refPoints, inputType)
    {
      Direction = direction;
    }

    public TriggerDirection Direction { get; private set; }
  }

  public class DumbArithmeticTriggerDependency : DumbArithmeticDependency, ITriggerDependency
  {
    public DumbArithmeticTriggerDependency(IReadOnlyCollection<IDependency> operands, Operator @operator, TriggerDirection direction) : base(operands, @operator)
    {
      Direction = direction;
    }

    public TriggerDirection Direction { get; private set; }
  }
}