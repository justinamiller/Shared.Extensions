using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Extensions
{
    internal static class Defaults
    {
        internal static int DefaultBufferSizeOnRead { get; } = 1024;
        internal static int DefaultBufferSizeOnWrite { get; } = 1024;
    }
}
