using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

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
    [XmlEnum(Name = "add")]
    Add,

    [XmlEnum(Name = "subtract")]
    Subtract,

    [XmlEnum(Name = "multiply")]
    Multiply,

    [XmlEnum(Name = "divide")]
    Divide,

    [XmlEnum(Name = "modulo")]
    Modulo
  }

  public enum TriggerDirection
  {
    [XmlEnum(Name = "add")]
    Up,
    [XmlEnum(Name = "down")]
    Down,
    [XmlEnum(Name = "both")]
    Both
  }
}