using System;

namespace Konayuki
{
    public class KonayukiOptions
    {
        protected static KonayukiOptions DefaultOptions = new KonayukiOptions();

        public static ref readonly KonayukiOptions Default => ref DefaultOptions;

        public KonayukiOptions()
        {
            
        }

        /// <value>
        /// Gets the epoch to use for <see cref="Konayuki"/>.
        /// </value>
        /// <remarks>
        /// When needed, this should be overridden in a derived class
        /// because the epoch should remain consistent across an application.
        /// </remarks>
        public virtual DateTime Epoch { get; } = new DateTime(2020, 12, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Recommended to be at least 40 or overflowing will very likely occur.
        /// </summary>
        /// <value>
        /// Gets the number of bits to use for the timestamp field.
        /// Defaults to 42 (139 years).
        /// </value>
        public byte TimestampBits { get; init; } = 42;

        /// <remarks>The minimum is 0, the maximum is 16.</remarks>
        /// <value>
        /// Gets the number of bits to use for the id field.
        /// Defaults to 8.
        /// </value>
        public byte IdBits { get; init; } = 8;

        /// <remarks>The maximum value is 32.</remarks>
        /// <value>
        /// Gets the number of bits to use for the sequence field.
        /// Defaults to 14.
        /// </value>
        public byte SequenceBits { get; init; } = 14;

        /// <value>
        /// Gets the interval in which the sequence field will reset to 0 in millseconds.
        /// Defaults to 1.
        /// </value>
        public double SequenceInterval { get; init; } = 1;

        /// <value>
        /// Gets the strategy to use when a sequence field overflows.
        /// Defaults to <see cref="SequenceOverflowStrategy.Throw"/>.
        /// </value>
        public SequenceOverflowStrategy SequenceOverflowStrategy { get; init; } = SequenceOverflowStrategy.Throw;
    }

    /// <summary>
    /// Strategies to handle sequence field overflows.
    /// </summary>
    public enum SequenceOverflowStrategy
    {
        /// <summary>
        /// Increment the timestamp value by 1.
        /// </summary>
        /// <remarks>This may cause collisions </remarks>
        IncrementTimestamp,
        /// <summary>
        /// Throws an exception.
        /// </summary>
        Throw
    }
}