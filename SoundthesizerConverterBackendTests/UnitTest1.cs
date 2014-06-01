using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoundthesizerConverterBackend;

namespace SoundthesizerConverterBackendTests
{
  [TestClass]
  public class SoundthesizerFileFormatTests
  {
    [TestMethod]
    public void LoadBR423TestFile()
    {
      soundset.LoadFromFile("BR423.xml");
    }
  }
}
