﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.Methods
{
    public static class MethodRepository
    {
        
        /// <summary>
        /// Skriv info in i Consolen med färg.
        /// </summary>
        /// <param name="text">Strängen du vill skriva ut.</param>
        /// <param name="färg">Färgen du vill använda.</param>
        /// <param name="somNyLinje">Om strängen ska vara på en ny rad eller på samma rad.</param>
        public static void PrintColor(string text, ConsoleColor färg = ConsoleColor.Gray, bool somNyLinje = true)
        {
            var oldCol = Console.ForegroundColor;
            Console.ForegroundColor = färg;

            if (somNyLinje)
                Console.WriteLine(text);
            else Console.Write(text);

            Console.ForegroundColor = oldCol;
        }
    }
}
