using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{

    internal class Lokal
    {
        private static List<string> bokningar = new List<string>();

        public static void BokaLokal(string lokalNamn)
        {
            bokningar.Add(lokalNamn);
            Console.WriteLine($"Lokal bokad för: {lokalNamn}");
        }

        public static void VisaBokningar()
        {
            Console.WriteLine("Bokade lokaler:");
            foreach (var bokning in bokningar)
            {
                Console.WriteLine(bokning);
            }
            

            // Else utifall lokal inte finns/bokad?
        }

        public static void AvbokaLokal(string lokalNamn)   // För att kunna avboka lokal
        {
            if (bokningar.Remove(lokalNamn))
            {
                Console.WriteLine($"Lokal avbokad för: {lokalNamn}");
            }
            else
            {
                Console.WriteLine($"Ingen bokning hittades för: {lokalNamn}");
            }
        }

        public static void SorteraBokningar()
        {
            bokningar = bokningar.OrderBy(b => b).ToList();
            Console.WriteLine("Bokningar sorterade alfabetiskt.");   // Sorterar bokningar i bokstavsordning
        }
    }


    // Ska lägga in sökmetod senare också
    class Program
    {
        static void Main(string[] args)   //Testar metoderna
        {
            Lokal.BokaLokal("Rum A");
            Lokal.BokaLokal("Rum B");
            Lokal.BokaLokal("Rum c");
            Lokal.BokaLokal("Rum D");
            Lokal.VisaBokningar();
        }
    }
    // Kommentera ev flera metoder som behövs
}



