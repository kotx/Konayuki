﻿using Konayuki;
using System;
using System.Linq;

namespace _01_basic_id_generation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IKonayuki<ulong> ids = new Konayuki.Konayuki(0);

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
            }
        }
    }
}
