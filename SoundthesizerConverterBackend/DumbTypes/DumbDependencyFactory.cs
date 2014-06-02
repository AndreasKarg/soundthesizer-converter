using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackend.DumbTypes
{
  public static class DumbDependencyFactory
  {
    public static IDependency Generate(Dependency dep)
    {
      if (dep == null)
        return null;

      if (dep.Value == "none")
        return null;

      if (dep.OperatorSpecified && (dep.Value != null))
        throw new SoundthesizerFileFormatException("Operator and Value must not be defined for the same Dependency.");

      //Hack: As the deserializer will always create an empty object even when the respective element is absent in the source file, assume an empty dependency to be absent.
      if (!dep.OperatorSpecified && (dep.Value == null) && (dep.Refpoints.Count == 0) && (dep.Dependencies.Count == 0))
        return null;

      if (!dep.OperatorSpecified && (dep.Value == null))
        throw new SoundthesizerFileFormatException("Either Operator or Value must be defined for a Dependency.");

      return dep.Value != null ? DumbValueDependencyFactory.Generate(dep) : DumbArithmeticDependencyFactory.Generate(dep);
    }
  }
}