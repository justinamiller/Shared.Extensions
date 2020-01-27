using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Extensions
{
    /// <summary>
    /// Simple interface to provide a generic mechanism to build guard clause extension methods from.
    /// </summary>
    public interface IGuardClause
    {
    }

    /// <summary>
    /// Indicates that the value of the marked element can never be <c>null</c>.
    /// </summary>
    /// <example><code>
    /// [NotNull] object Foo() {
    ///   return null; // Warning: Possible 'null' assignment
    /// }
    /// </code></example>
    [AttributeUsage(
      AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
      AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
      AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    internal sealed class NotNullAttribute : Attribute { }

    /// <summary>
    /// An entry point to a set of Guard Clauses defined as extension methods on IGuardClause.
    /// </summary>
    public class Guard : IGuardClause
    {
        /// <summary>
        /// An entry point to a set of Guard Clauses.
        /// </summary>
        [NotNull] public static IGuardClause Against { get; } = new Guard();

        private Guard() { }
    }
}
