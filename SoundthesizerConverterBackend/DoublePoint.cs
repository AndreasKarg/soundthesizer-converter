namespace SoundthesizerConverterBackend
{
  public struct DoublePoint
  {
    public DoublePoint(double x, double y) : this()
    {
      Y = y;
      X = x;
    }

    public double X { get; private set; }
    public double Y { get; private set; }
  }
}