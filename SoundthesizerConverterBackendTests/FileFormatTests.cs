using System;
using System.CodeDom;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoundthesizerConverterBackend;
using SoundthesizerConverterBackend.DumbTypes;
using SoundthesizerConverterBackend.Xml;

namespace SoundthesizerConverterBackendTests
{
  [TestClass]
  public class SoundthesizerFileFormatTests
  {
    [TestMethod]
    public void LoadBR423TestFile()
    {
      var result = Soundset.LoadFromFile("Test Files\\BR423.xml");

      Assert.AreEqual("BR 423", result.Name);
      Assert.AreEqual(9, result.Sounds.Count);
      Assert.IsTrue(Math.Abs(71 - result.Sounds[1].Volume.Dependencies[1].Refpoints[4].X) < 1);
    }

    [TestMethod]
    public void LoadEmptyTestFile()
    {
      var result = Soundset.LoadFromFile("Test Files\\EmptyTestFile.xml");

      Assert.AreEqual("BR 423", result.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException), "The value 'FAIL' for the 'operator' attribute in the test file was inappropriately allowed.")]
    public void LoadEnumTestFile()
    {
      Soundset.LoadFromFile("Test Files\\EnumTestFile.xml");
    }

    [TestMethod]
    public void GenerateDumbSoundSetFromBR423()
    {
      var soundSet = DumbSoundsetFactory.GenerateFromSoundthesizer("Test Files\\BR423.xml");
      Assert.AreEqual("BR 423", soundSet.Name);
      Assert.AreEqual(9, soundSet.Sounds.Count);
      Assert.IsTrue(Math.Abs(71 - ((soundSet.Sounds.ElementAt(1).Volume as IArithmeticDependency).Operands.ElementAt(1) as IValueDependency).RefPoints.ElementAt(4).X) < 1);
    }
  }
}
