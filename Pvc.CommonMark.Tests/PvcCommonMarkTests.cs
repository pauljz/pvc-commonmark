using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PvcCore;
using System.Collections.Generic;
using System.IO;

namespace Pvc.CommonMark.Tests
{
    [TestClass]
    public class PvcCommonMarkTests
    {
        /// <summary>
        /// Just test that the plugin is parsing. Full CommonMark spec compliance is tested in the CommonMark.NET package itself.
        /// </summary>
        [TestMethod]
        public void TestConvert()
        {
            var plugin = new PvcPlugins.PvcCommonMark();

            var inputStream = PvcUtil.StringToStream("Test file!", "test.md");
            inputStream.Seek(0, SeekOrigin.Begin);

            var inputStreams = new List<PvcStream>() { inputStream };
            var outputStreams = new List<PvcStream>(plugin.Execute(inputStreams));

            var outputStream = outputStreams[0];
            outputStream.Seek(0, SeekOrigin.Begin);
            var html = new StreamReader(outputStream).ReadToEnd();

            Assert.IsTrue(html.Contains("<p>Test file!</p>"));
        }
    }
}
