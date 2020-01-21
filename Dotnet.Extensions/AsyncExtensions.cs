using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Extensions
{
    public static class AsyncExtensions
    {
        public static Task CancelIfRequestedAsync(this CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested ? FromCanceled(cancellationToken) : null;
        }

        // From 4.6 on we could use Task.FromCanceled(), but we need an equivalent for
        // previous frameworks.
        public static Task FromCanceled(this CancellationToken cancellationToken)
        {
            return new Task(() => { }, cancellationToken);
        }

        public static Task<T> FromCanceled<T>(this CancellationToken cancellationToken)
        {
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
            return new Task<T>(() => default, cancellationToken);
#pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
        }


        public static Task WriteAsync(this TextWriter writer, char value, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested ? FromCanceled(cancellationToken) : writer.WriteAsync(value);
        }

        public static Task WriteAsync(this TextWriter writer, string value, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested ? FromCanceled(cancellationToken) : writer.WriteAsync(value);
        }

        public static Task WriteAsync(this TextWriter writer, char[] value, int start, int count, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested ? FromCanceled(cancellationToken) : writer.WriteAsync(value, start, count);
        }

        public static Task<int> ReadAsync(this TextReader reader, char[] buffer, int index, int count, CancellationToken cancellationToken)
        {
            return cancellationToken.IsCancellationRequested ? FromCanceled<int>(cancellationToken) : reader.ReadAsync(buffer, index, count);
        }
    }
}
