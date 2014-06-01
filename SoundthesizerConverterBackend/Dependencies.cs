using System.Collections.Generic;
using System.Drawing;

namespace SoundthesizerConverterBackend
{
  public interface IDependency
  {
  }

  public interface IValueDependency : IDependency
  {
    IReadOnlyCollection<DoublePoint> RefPoints { get; }
    InputType InputType { get; }
  }

  public interface IArithmeticDependency : IDependency
  {
    IReadOnlyCollection<IDependency> Operands { get; }
    Operator Operator { get; }
  }

  public interface ITriggerDependency : IDependency
  {
    TriggerDirection Direction { get; }
  }

  public enum Operator
  {
    Add, Subtract, Multiply, Divide, Modulo
  }

  public enum TriggerDirection
  {
    Up, Down, Both
  }
}