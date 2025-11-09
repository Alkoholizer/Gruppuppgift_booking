using Gruppuppgift_booking.filer;
using Gruppuppgift_booking.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    interface IBookable //Den abstrakta Bookable-klassen, som skall implementeras som returtyp vid bokning.
    {
        abstract static void NewBooking();

        abstract static void UpdateBooking();

        abstract static void CancelBooking();
    }

    public record BookingData
    (
        int ID,
        string CustomerName,

        DateTime StartTime,
        DateTime EndTime,

        int LokalID
    );

    public class Booking : IBookable
    {
        private static int IncrementalID;

        public const string FILENAME = "bokningar.json";

        public Booking(string _customerName, DateTime start, DateTime end)
        {
            ID = IncrementalID++;
            CustomerName = _customerName;
            StartTime = start;
            EndTime = end;
        }

        public readonly int ID;
        public string CustomerName { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public Lokal? MinLokal { get; private set; }
        public void BokaLokal(Lokal? lokal)
        {
            MinLokal = lokal;
        }

        public string GetBookingData()
        {
            return $"[{ID}]: \"{CustomerName}\" {StartTime} to {EndTime}";
        }


        public BookingData ToBookingData()
        {
            return new BookingData
            (
                ID,
                CustomerName,

                StartTime,
                EndTime,

                MinLokal != null ? MinLokal.ID : 0
            );
        }


        


        public static List<Booking> Bokningar { get; private set; } = [];

		public static void NewBooking()
		{
            Lokal.VisaLokaler(true);

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

		RedoLokal:
            MethodRepository.PrintColor($"Ange en lokal att boka (0-{Lokal.Lokaler.Count}): ", ConsoleColor.Cyan);
            if (!int.TryParse(Console.ReadLine(), out int lokalID) && lokalID < 0 || lokalID >= Lokal.Lokaler.Count)
            {
                MethodRepository.PrintColor("Ange ett giltigt lokals ID!", ConsoleColor.Red);
                goto RedoLokal;
            }

            Lokal lok = Lokal.Lokaler[lokalID];

            var book = new Booking(customer, startdatum, slutdatum);

            lok.SetBokning(book);
            book.BokaLokal(lok);
            Bokningar.Add(book);

            Console.WriteLine($"Lokalen \"{lok.Namn}\" bokad för: {customer}");

            SparaBokningar();
		}

		public static void CancelBooking()
		{
		Redo:
            Console.Clear();

            VisaBokningar();
            Console.WriteLine("Skriv \"avbryt för att gå tillbaka.");
            MethodRepository.PrintColor($"Ange en lokal att boka (0-{Bokningar.Count}): ", ConsoleColor.Cyan);

            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                goto Redo;
            }

            if (input.ToLower() == "avbryt")
            {
                Console.Clear();
                Program.Start(1);
                return;
            }

            if (!int.TryParse(input, out int bookingID) || 
                Bokningar.FirstOrDefault(x => x.ID == bookingID) is not Booking book)
            {
                MethodRepository.PrintColor("Ange ett giltigt lokals ID!", ConsoleColor.Red);
                goto Redo;
            }

            // Vi tar bork bokningen från en lokal.
            book.MinLokal?.SetBokning(null);
            Bokningar.Remove(book);
            SparaBokningar();
		}

		public static void UpdateBooking()
		{
		Redo:
            Console.Clear();
            
            VisaBokningar();

            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                goto Redo;
            }

            if (input.ToLower() == "avbryt")
            {
                Console.Clear();
                Program.Start(1);
                return;
            }

            if (!int.TryParse(input, out int bookingID) || 
                Bokningar.FirstOrDefault(x => x.ID == bookingID) is not Booking book)
            {
                MethodRepository.PrintColor("Ange ett giltigt boknings ID!", ConsoleColor.Red);
                goto Redo;
            }

            // Bokning hittad, nu kan vi ändra den!

            // TODO: Ändra bokningens data här!


            // Rör ej!
            Console.WriteLine(book.GetBookingData());
            SparaBokningar();
		}

        
        // Rör ej, den här är den enda funktionen som behövs för att spara bokningar!
        public static void SparaBokningar()
        {
            List<BookingData> bookDatas = [];

            foreach(var lok in Bokningar)
                bookDatas.Add(lok.ToBookingData());

            FilHantering.WriteJson
            (
                FILENAME, 
                bookDatas, 
                new System.Text.Json.JsonSerializerOptions()
                {
                    WriteIndented = true
                }
            );
        }



        public static void VisaBokningar()
        {
            if (Bokningar.Count < 1)
            {
                MethodRepository.PrintColor("Inga bokningar sparade!", ConsoleColor.Red);
                return;
            }

            Console.WriteLine("Bokade lokaler:");
            foreach (var bokning in Bokningar)
            {
                Console.WriteLine(bokning.GetBookingData());
            }
        }

        public static void SorteraBokningar()
        {
            Bokningar = [.. Bokningar.OrderBy(b => b)];
            Console.WriteLine("Bokningar sorterade alfabetiskt.");   // Sorterar bokningar i bokstavsordning
        }
    

    
    }
}
