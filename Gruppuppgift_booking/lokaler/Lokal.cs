﻿using Gruppuppgift_booking.Methods;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public record LokalData
    (
        LokalTyp Typ,

        string Namn,
        double Area,

        int Nummer,
        bool HarSoffa,
        bool HarProjector
    );

    public class Lokal
    {
        private static List<Booking> Bokningar = [];
        public static readonly List<Lokal> Lokaler = [];

        public Lokal(LokalData data)
        {
            Namn = data.Namn;
            Typ = data.Typ;
        }

        public string Namn;
        public LokalTyp Typ;
        public double Area;

        public Booking? Bokning;


        public static void BokaLokal()
        {
        Redo:
            Console.Write("Ange kundens namn: ");
            var customer = Console.ReadLine();

            if (string.IsNullOrEmpty(customer))
                goto Redo;
            
            RedoStart:
            Console.Write("Ange startdatum: ");
            var datestart = Console.ReadLine();

            if (DateTime.TryParse(datestart, out DateTime startdatum))
                goto RedoStart;

            RedoEnd:
            Console.Write("Ange slutdatum: ");
            var dateend = Console.ReadLine();

            if (DateTime.TryParse(dateend, out DateTime slutdatum))
                goto RedoEnd;




            Lokal lok = Lokaler[0];

            Console.WriteLine($"Lokal bokad för: {lok.Namn}");

            lok.Bokning = new Booking(customer, startdatum, slutdatum);
            lok.Bokning.BokaLokal(lok);
            Bokningar.Add(lok.Bokning);


        }

        public static void VisaBokningar()
        {
            Console.WriteLine("Bokade lokaler:");
            foreach (var bokning in Bokningar)
            {
                Console.WriteLine(bokning.GetBookingData());
            }

            // Else utifall lokal inte finns/bokad?
        }

        public static void AvbokaLokal()   // För att kunna avboka lokal
        {
            if (Bokningar.Count < 1)
                return;

            VisaBokningar();

        Redo:
            Console.Write("\nSkriv \"avbryt\" för att avbryta.\nAnge bokningens ID:");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                goto Redo;

            if (input.ToLower() == "avbryt")
            {
                return;
            }

            if (int.TryParse(input, out int id))
                goto Redo;

            if (Bokningar.FirstOrDefault(x => x.ID == id) is Booking bok)
            {
                bok.MinLokal.Bokning = null;
                Bokningar.Remove(bok);
            }
            else
            {
                MethodRepository.PrintColor($"Hittade inte en bokning med ID: {id}", ConsoleColor.Red);
                goto Redo;
            }
        }

        public static void SorteraBokningar()
        {
            Bokningar = Bokningar.OrderBy(b => b).ToList();
            Console.WriteLine("Bokningar sorterade alfabetiskt.");   // Sorterar bokningar i bokstavsordning
        }
        public void UniqueCheck(string input)
        {
            foreach (Lokal lok in Lokaler)
            {
                if (input == Namn)
                {
                    Console.WriteLine("Fel, en lokal måste ha ett unikt namn.");
                    Console.ReadLine();
                    break;
                }
            }
        }
    }

    public enum LokalTyp
    {
        Lokal = 1,
        Grupprum = 2,
        Sal = 3,
    }
}   