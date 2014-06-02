using System.Collections.Generic;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public class DumbArithmeticDependency : IArithmeticDependency
  {
    private readonly IReadOnlyCollection<IDependency> _operands;

    public DumbArithmeticDependency(IReadOnlyCollection<IDependency> operands, Operator @operator)
    {
      _operands = operands;
      Operator = @operator;
    }

    public IReadOnlyCollection<IDependency> Operands
    {
      get { return _operands; }
    }

    public Operator Operator { get; private set; }
  }
}