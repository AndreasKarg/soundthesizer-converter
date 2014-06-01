using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoundthesizerConverterBackend;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackendTests
{
  [TestClass]
  public class SoundthesizerFileFormatTests
  {
    [TestMethod]
    public void LoadBR423TestFile()
    {
      var result = soundset.LoadFromFile("Test Files\\BR423.xml");
    }

    [TestMethod]
    public void LoadEmptyTestFile()
    {
      var result = soundset.LoadFromFile("Test Files\\EmptyTestFile.xml");

      Assert.AreEqual("BR 423", result.name);
    }

    [TestMethod]
    public void GenerateDumbSoundSetFromBR423()
    {
      var soundSet = DumbSoundsetFactory.GenerateSoundsetFromFile("Test Files\\BR423.xml");
    }
  }
}
