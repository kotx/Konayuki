using Konayuki;
using System;

namespace _02_custom_epoch
{
    internal class MyKonayukiOptions : KonayukiOptions
    {
        /// <value>
        /// Gets the overridden epoch set to January 1st, 2020.
        /// </value>
        public override DateTime Epoch { get; } = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
