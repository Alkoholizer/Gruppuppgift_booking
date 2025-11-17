using Gruppuppgift_booking.filer;
using Gruppuppgift_booking.Methods;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        abstract static void ListBookingsByYear();
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
        private static int IncrementalID = 0;

        public const string FILENAME = "bokningar.json";

        public Booking(string _customerName, DateTime start, DateTime end)
        {
            IncrementalID++;
            ID = IncrementalID;

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
            return $"[{ID}]: \"{CustomerName}\", Datum: {
                StartTime:MMMM dd, yyyy} till {
                EndTime:MMMM dd, yyyy}";
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
            Console.Clear();
            MethodRepository.PrintColor("===NY BOKNING===", ConsoleColor.Cyan);
            
            if (Lokal.Lokaler.Count < 1)
            {
                MethodRepository.PrintColor("Det finns inga lokaler att boka!", ConsoleColor.Red);
                Program.ReturnFromMenu(MenyTyp.Bokningar);
                return;
            }

            Console.WriteLine();

		Redo:
            Console.Write("Ange kundens namn: ");
            var customer = Console.ReadLine();
            if (string.IsNullOrEmpty(customer))
                goto Redo;
            
        RedoStart:
            Console.Write("Ange startdatum (yyyy-MM-dd): ");
            var datestart = Console.ReadLine();
            if (!DateTime.TryParseExact(datestart,
                       ["yyyy-MM-dd"],
                       new CultureInfo("en-SE"),
                       DateTimeStyles.None,
                       out DateTime startdatum) || startdatum.Subtract(DateTime.Now).TotalSeconds <= 0)
                goto RedoStart;

        RedoEnd:
            Console.Write("Ange slutdatum: (yyyy-MM-dd): ");
            var dateEnd = Console.ReadLine();
            if (!DateTime.TryParseExact(dateEnd,
                       ["yyyy-MM-dd"],
                       new CultureInfo("en-SE"),
                       DateTimeStyles.None,
                       out DateTime slutdatum) || startdatum.Subtract(slutdatum).TotalSeconds > 0)
                goto RedoEnd;
            
            Console.WriteLine();
            Lokal.VisaLokaler(false, true);
            Console.WriteLine();

		RedoLokal:
            Console.Write($"Ange en lokal att boka: ");
            if (!int.TryParse(Console.ReadLine(), out int lokalID))
            {
                MethodRepository.PrintColor("\nAnge ett giltigt lokals ID!", ConsoleColor.Red);
                goto RedoLokal;
            }

            Lokal? lok = Lokal.Lokaler.FirstOrDefault(x => x.ID == lokalID);

            if (lok == null)
            {
                MethodRepository.PrintColor("\nAnge ett giltigt lokals ID!", ConsoleColor.Red);
                goto RedoLokal;
            }

            var book = new Booking(customer, startdatum, slutdatum);

            lok.SetBokning(book);
            book.BokaLokal(lok);
            Bokningar.Add(book);

            Console.WriteLine($"Lokalen \"{lok.Namn}\" bokad för: {customer}");

            SparaBokningar();

            Program.ReturnFromMenu(MenyTyp.Bokningar);
		}

		public static void CancelBooking()
		{
		Redo:
            Console.Clear();
            MethodRepository.PrintColor("===TA BORT BOKNING===", ConsoleColor.Cyan);

            VisaBokningar(true);

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
                Program.Start(MenyTyp.Bokningar);
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

            Program.ReturnFromMenu(MenyTyp.Bokningar);
		}

		public static void UpdateBooking()
		{
		Redo:
            Console.Clear();
            MethodRepository.PrintColor("===UPPDATERA BOKNINGAR===", ConsoleColor.Cyan);
            
            VisaBokningar(true);

            Console.Write("\nAnge ID på en bokning att ändra: ");
            if (!int.TryParse(Console.ReadLine(), out int bookingID) || 
                Bokningar.FirstOrDefault(x => x.ID == bookingID) is not Booking book)
            {
                MethodRepository.PrintColor("Ange ett giltigt boknings ID!", ConsoleColor.Red);
                goto Redo;
            }

            Console.WriteLine($"Nu ändrar vi bokning nr {book.ID} som ägs av {book.CustomerName}.\n");

            //Ny kod av John. För att uppdatera bokningen.
            MethodRepository.PrintColor("Vad vill du ändra?", ConsoleColor.Cyan);
	        Console.WriteLine("1: Namn");
	        Console.WriteLine("2: Start Tid");
	        Console.WriteLine("3: Sluttid");
	        Console.WriteLine("4: Lokal");

            Console.Write("Ändring val: ");
	        if (!int.TryParse(Console.ReadLine(), out int choice))
	        {
		        MethodRepository.PrintColor("Du måste ange ett giltigt nummer!", ConsoleColor.Red);
	        }

            switch ((BookingDataType)choice)
		    {
			    case BookingDataType.Namn:
				    Console.Write("Ange nytt namn: ");
				    string? newName = Console.ReadLine();
				    if (!string.IsNullOrWhiteSpace(newName))
				    {
					    book.CustomerName = newName;
					    Console.WriteLine("Namn uppdaterat!");
				    }
				    else
				    {
					    MethodRepository.PrintColor("Ogiltigt namn!", ConsoleColor.Red);
				    }
				    break;

			    case BookingDataType.StartTid:
                    Console.Write("Ange ny starttid (yyyy-MM-dd): ");
                    var newStart = Console.ReadLine();
                    if (DateTime.TryParseExact(newStart,
                               ["yyyy-MM-dd"],
                               new CultureInfo("en-SE"),
                               DateTimeStyles.None,
                               out DateTime newStartTime) && DateTime.Now.Subtract(newStartTime).TotalSeconds < 0)
				    {
					    book.StartTime = newStartTime;
					    Console.WriteLine("Starttid uppdaterad!");
				    }
				    else
				    {
					    MethodRepository.PrintColor("Ogiltig starttid!", ConsoleColor.Red);
				    }
				    break;

			    case BookingDataType.SlutTid:
                    Console.Write("Ange ny sluttid (yyyy-MM-dd): ");
                    var newEnd = Console.ReadLine();
                    if (DateTime.TryParseExact(newEnd,
                               ["yyyy-MM-dd"],
                               new CultureInfo("en-SE"),
                               DateTimeStyles.None,
                               out DateTime newEndTime) && newEndTime.Subtract(book.StartTime).TotalSeconds > 0
                    )
				    {
					    book.EndTime = newEndTime;
					    Console.WriteLine("Sluttid uppdaterad!");
				    }
				    else
				    {
					    MethodRepository.PrintColor("Ogiltig sluttid!", ConsoleColor.Red);
				    }
				    break;

			    case BookingDataType.Lokal:
				    Console.Write("Ange ID för ny lokal: ");
				    if (int.TryParse(Console.ReadLine(), out int lokalId))
				    {
					    var newLokal = Lokal.Lokaler.FirstOrDefault(l => l.ID == lokalId);
					    if (newLokal != null)
					    {
						    book.MinLokal?.SetBokning(null);// Koppla bort från gammal lokal
						    newLokal.SetBokning(book);      // Koppla till ny lokal
						    book.BokaLokal(newLokal);
						    Console.WriteLine("Lokal uppdaterad!");
					    }
					    else
					    {
						    MethodRepository.PrintColor("Lokal hittades inte!", ConsoleColor.Red);
					    }
				    }
				    else
				    {
					    MethodRepository.PrintColor("Ogiltigt ID!", ConsoleColor.Red);
				    }
				    break;

			    default:
				    MethodRepository.PrintColor("Ogiltigt val!", ConsoleColor.Red);
                    break;
		    }


            // Rör ej!
            Console.WriteLine(book.GetBookingData());
            SparaBokningar();

            Program.ReturnFromMenu(MenyTyp.Bokningar);
		}
        
        public enum BookingDataType
	    {
		    Namn = 1,
		    StartTid = 2,
		    SlutTid = 3,
		    Lokal = 4
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



        public static void VisaBokningar(bool fromOther)
        {
            if (!fromOther)
            {
                Console.Clear();
                MethodRepository.PrintColor("===BOKADE LOKALER===", ConsoleColor.Cyan);
            }

            if (Bokningar.Count < 1)
            {
                MethodRepository.PrintColor("Inga bokningar sparade!", ConsoleColor.Red);
                Program.ReturnFromMenu(MenyTyp.Bokningar);
                return;
            }

            foreach (var bokning in Bokningar)
            {
                Console.WriteLine(bokning.GetBookingData());
            }

            if (!fromOther)
                Program.ReturnFromMenu(MenyTyp.Bokningar);
        }

        public static void SorteraBokningar()
        {
            Bokningar = [.. Bokningar.OrderBy(b => b)];
            Console.WriteLine("Bokningar sorterade alfabetiskt.");   // Sorterar bokningar i bokstavsordning
        }

		public static void ListBookingsByYear()
		{
            Console.Clear();
            MethodRepository.PrintColor("===LISTA BOKNINGAR PÅ ÅR===", ConsoleColor.Cyan);
            
		Redo:
            Console.Write("\nAnge år att söka efter: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                MethodRepository.PrintColor("Ogiltigt år!", ConsoleColor.Red);
                goto Redo;
            }

            List<Booking> booksInYear = [];
            foreach(var book in Bokningar)
            {
                if (book.StartTime.Year == year)
                {
                    booksInYear.Add(book);
                }
            }

            if (booksInYear.Count < 1)
                MethodRepository.PrintColor("Inga bokningar på detta året!", ConsoleColor.Yellow);

            foreach(var book in booksInYear)
            {
                Console.WriteLine(book.GetBookingData());
            }
            
            Program.ReturnFromMenu(MenyTyp.Bokningar);
		}
	}
}
