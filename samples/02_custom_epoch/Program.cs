using System;
using System.Linq;

namespace _02_custom_epoch
{
    internal class Program
    {
        private static void Main()
        {
            var defaultIds = new Konayuki.Konayuki(0);
            var customIds = new Konayuki.Konayuki(0, new MyKonayukiOptions());

            Console.WriteLine($"Here is an ID with the epoch set to the default, {defaultIds.Options.Epoch}: {defaultIds.First()}");

            Console.WriteLine($"Here is an ID with the epoch set to {customIds.Options.Epoch}: {customIds.First()}");
        }
    }
}
