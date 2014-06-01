using System.Collections.Generic;

namespace SoundthesizerConverterBackend
{
  public class DumbArithmeticDependency : IArithmeticDependency, ITriggerDependency
  {
    private readonly IReadOnlyCollection<IDependency> _operands;

    public DumbArithmeticDependency(IReadOnlyCollection<IDependency> operands, Operator @operator, TriggerDirection direction = TriggerDirection.Both)
    {
      _operands = operands;
      Direction = direction;
      Operator = @operator;
    }

    public IReadOnlyCollection<IDependency> Operands
    {
      get { return _operands; }
    }

    public Operator Operator { get; private set; }
    public TriggerDirection Direction { get; private set; }
  }
}