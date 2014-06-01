using System;
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

      Assert.AreEqual("BR 423", result.name);
      Assert.AreEqual(9, result.Sounds.Count);
      Assert.IsTrue(Math.Abs(71 - result.Sounds[1].volume.dependency[1].refpoint[4].x) < 1);
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
