using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace Konayuki
{
    public class Konayuki : IKonayuki<ulong>, IDisposable
    {
        public Konayuki(ushort id) : this(id, KonayukiOptions.Default)
        { }

        public Konayuki(ushort id, KonayukiOptions options)
        {
            Id = id;
            Options = options;

            if (options.TimestampBits + options.IdBits + options.SequenceBits != 64)
                throw new ArgumentOutOfRangeException($"Number of bits in {nameof(options)} must equal 64.");

            Timer = new System.Timers.Timer(Options.SequenceInterval) { AutoReset = true };
            Timer.Elapsed += (sender, e) => {
                lock (SequenceLocker)
                    Sequence = 0;
            };
        }

        /// <value>
        /// Gets the generator ID for Konayuki.
        /// </value>
        public ushort Id { get; }

        /// <value>
        /// Gets the <see cref="KonayukiOptions">options</see> for this Konayuki instance.
        /// </value>
        public KonayukiOptions Options { get; }

        /// <value>
        /// Gets the internal Timer instance used for resetting the <see cref="Sequence"/>.
        /// </value>
        protected System.Timers.Timer Timer { get; set; }

        /// <value>
        /// Gets the current sequence value.
        /// </value>
        /// <remarks>Must lock <see cref="SequenceLocker"/> before accessing!</remarks>
        protected uint Sequence { get; set; } = 0;
        protected static readonly object SequenceLocker = new object();

        /// <summary>
        /// Generates an ID.
        /// </summary>
        /// <returns>The generated ID.</returns>
        protected ulong Generate()
        {
            ulong timestamp = (ulong) (DateTime.UtcNow - Options.Epoch).TotalMilliseconds;

            lock (SequenceLocker)
            {
                if (((uint) Math.Log(Sequence, 2) + 1) > Options.SequenceBits)
                {
                    switch (Options.SequenceOverflowStrategy)
                    {
                        case SequenceOverflowStrategy.IncrementTimestamp:
                            timestamp++;
                            break;
                        case SequenceOverflowStrategy.Throw:
                            throw new OverflowException("Cannot generate any more IDs in this sequence interval.");
                    }
                }

                return
                    ((timestamp) << (Options.IdBits + Options.SequenceBits)) |
                    ((ulong) Id << Options.SequenceBits) |
                    Sequence++;
            }
        }

        /// <summary>
        /// Returns an infinite <see cref="IEnumerator"/> of IDs.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> to iterate over IDs.</returns>
        public IEnumerator<ulong> GetEnumerator()
        {
            while (true)
                yield return Generate();
        }

        /// <summary>
        /// Returns an infinite <see cref="IEnumerator"/> of IDs.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> to iterate over IDs.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Timer.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
