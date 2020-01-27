using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Extensions.TestFramework
{
    [TestClass]
    public  class GuardExtentionsTest
    {
        [TestMethod]
        public void DoesNothingGivenNonDefaultValue()
        {
            Guard.Against.Default("", "string");
            Guard.Against.Default(1, "int");
            Guard.Against.Default(Guid.NewGuid(), "guid");
            Guard.Against.Default(DateTime.Now, "datetime");
            Guard.Against.Default(new Object(), "object");
        }

        [TestMethod]
        public void DoesNothingGivenNonNullValue()
        {
            Guard.Against.Null("", "string");
            Guard.Against.Null(1, "int");
            Guard.Against.Null(Guid.Empty, "guid");
            Guard.Against.Null(DateTime.Now, "datetime");
            Guard.Against.Null(new Object(), "object");
        }

        [TestMethod]
        public void DoesNothingGivenNonEmptyStringValue(string nonEmptyString)
        {
            Guard.Against.NullOrWhiteSpace(nonEmptyString, "string");
            Guard.Against.NullOrWhiteSpace(nonEmptyString, "aNumericString");
        }
        [TestMethod]
        public void DoesNothingGivenPositiveValue()
        {
            Guard.Against.NegativeOrZero(1, "intPositive");
            Guard.Against.NegativeOrZero(1L, "longPositive");
            Guard.Against.NegativeOrZero(1.0M, "decimalPositive");
            Guard.Against.NegativeOrZero(1.0f, "floatPositive");
            Guard.Against.NegativeOrZero(1.0, "doublePositive");
        }

        [TestMethod]
        public void DoesNothingGivenNonEmptyStringValue()
        {
            Guard.Against.NullOrEmpty("a", "string");
            Guard.Against.NullOrEmpty("1", "aNumericString");
        }

        [TestMethod]
        public void DoesNothingGivenNonEmptyEnumerable()
        {
            Guard.Against.NullOrEmpty(new[] { "foo", "bar" }, "stringArray");
            Guard.Against.NullOrEmpty(new[] { 1, 2 }, "intArray");
        }

        [TestMethod]
        public void DoesNothingGivenNonZeroValue()
        {
            Guard.Against.Zero(-1, "minusOne");
            Guard.Against.Zero(1, "plusOne");
            Guard.Against.Zero(int.MinValue, "int.MinValue");
            Guard.Against.Zero(int.MaxValue, "int.MaxValue");
            Guard.Against.Zero(long.MinValue, "long.MinValue");
            Guard.Against.Zero(long.MaxValue, "long.MaxValue");
            Guard.Against.Zero(decimal.MinValue, "decimal.MinValue");
            Guard.Against.Zero(decimal.MaxValue, "decimal.MaxValue");
            Guard.Against.Zero(float.MinValue, "float.MinValue");
            Guard.Against.Zero(float.MaxValue, "float.MaxValue");
            Guard.Against.Zero(double.MinValue, "double.MinValue");
            Guard.Against.Zero(double.MaxValue, "double.MaxValue");
        }

    }
}
