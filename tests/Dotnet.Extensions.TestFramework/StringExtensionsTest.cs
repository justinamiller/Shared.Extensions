using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dotnet.Extensions.TestFramework
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void IsNullOrEmptyTest()
        {
            Assert.IsTrue("".IsNullOrEmpty());
            Assert.IsTrue(((string)null).IsNullOrEmpty());
            Assert.IsFalse("Hi".IsNullOrEmpty());
        }
    }
}
