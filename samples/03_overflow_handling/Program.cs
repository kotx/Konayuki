using Konayuki;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_overflow_handling
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string input;

            if (args.Length > 0)
                input = args[0];
            else
            {
                Console.Write("How many IDs do you want to generate? ");
                input = Console.ReadLine();
            }

            if (!int.TryParse(input, out int count) || count <= 0)
            {
                Console.WriteLine("You didn't enter a valid number (Int32), so I'll be generating just one ID.");
                count = 1;
            }

            string type;

            if (args.Length > 0)
            {
                type = args[1];
            }
            else
            {
                Console.Write("How do you want to handle sequence field overflows? ");
                type = Console.ReadLine();
            }

            if (!Enum.TryParse(typeof(SequenceOverflowStrategy), type, out object strategy) || strategy == null)
            {
                Console.WriteLine($"You didn't enter a valid {nameof(SequenceOverflowStrategy)}.");
                strategy = SequenceOverflowStrategy.IncrementTimestamp;
            }

            Console.WriteLine($"I'll be using the {strategy} strategy.");

            IKonayuki<ulong> ids = new Konayuki.Konayuki(0, new KonayukiOptions 
            {
                SequenceOverflowStrategy = (SequenceOverflowStrategy) strategy
            });

            Console.WriteLine("Here are your ID(s), please wait:");
            Console.WriteLine("---------------------");

            if (count == 1)
                // Use .First() if you want to take one ID at a time.
                Console.WriteLine(ids.First());
            else
            {
                var result = ids.Take(count);

                // Avoid excessive memory use.
                if (count > 1000000)
                {
                    foreach (ulong id in result)
                        Console.WriteLine(id);
                }
                else
                {
                    // This is faster than writing in a loop due to console shenanigans, but causes increased memory use.
                    Console.WriteLine(string.Join(Environment.NewLine, result));
                }

                // The following code block was taken from https://stackoverflow.com/a/53512576.

                var hashset = new HashSet<ulong>(); // to determine if we already have seen this id
                var duplicates = new Dictionary<ulong, int>(); // will contain the ids that are duplicates
                foreach (var id in result)
                {
                    if (!hashset.Add(id))
                    {
                        duplicates.TryGetValue(id, out int idCount); // get the # of times this id has been duplicated
                        duplicates.Add(id, idCount + 1);
                    }
                }

                Console.WriteLine($"Of the {count} IDs, {duplicates.Count} were duplicates. The duplicates were: {string.Join(Environment.NewLine, duplicates)}");
            }
        }
    }
}
