using Gruppuppgift_booking.filer;
using Gruppuppgift_booking.lokaler;
using Gruppuppgift_booking.Methods;

namespace Gruppuppgift_booking
{
    internal class Program
    {

        static void Main(string[] args)
        {
            FilHantering.Init();

            if (FilHantering.ReadJson(out List<LokalData>? lokaler, out Exception? exc, Lokal.FILENAME) && lokaler != null)
            {
                foreach(var lokData in lokaler)
                {
                    switch(lokData.Typ)
                    {
                        case LokalTyp.Lokal: new Lokal(lokData); break;
                        case LokalTyp.Sal: new Sal(lokData); break;
                        case LokalTyp.Grupprum: new Grupprum(lokData); break;
                    }
                }
            }

            if (FilHantering.ReadJson(out List<BookingData>? bokningar, out exc, Booking.FILENAME) && lokaler != null)
            {
                foreach(var d in bokningar)
                {
                    var book = new Booking(d.CustomerName, d.StartTime, d.EndTime);
                    if (d.LokalID > 0)
                    {
                        var lokal = Lokal.Lokaler.FirstOrDefault(x => x.ID == d.LokalID);
                        if (lokal != null)
                            book.BokaLokal(lokal);
                    }
                    Booking.Bokningar.Add(book);
                }
            }

            Start();
        }

        public static void Start(int menuIndex = -1)
        {
            int val = menuIndex;
            if (val < 0)
            {
                Console.WriteLine
                (
                    "Välkommen till bokningssystemet!\n" +
                    "Mata in en siffra för att välja." +
                    "\n1: Hantera bokningar " +
                    "\n2: Hantera lokaler"
                );
                val = MenyVal(2, true);
            }

            switch(val)
            {
                case 1: ManageBookings(); break;
                case 2: HandleRooms(); break;
            }

            void ManageBookings()
            {
                Console.WriteLine("1: Skapa ny bokning " +
                                "\n2: Lista alla bokningar " +
                                "\n3: Uppdatera bokning " +
                                "\n4: Ta bort en bokning " +
                                "\n0: Gå tillbaka"
                                );

                switch (MenyVal(5))
                {
                    case 1: Booking.NewBooking(); break;
                    case 2: Booking.VisaBokningar(); break;
                    case 3: Booking.UpdateBooking(); break;
                    case 4: Booking.CancelBooking(); break;
                    case 0: Console.Clear(); Start(); break;
                }
            }
            void HandleRooms()
            {
                Console.WriteLine("1: Lista alla lokaler " +
                                "\n2: Skapa ny lokal" +
                                "\n3: Ta bort en lokal" +
                                "\n0: Gå tillbaka"
                                );

                switch (MenyVal(5))
                {
                    case 1: Lokal.VisaLokaler(true); break;
                    case 2: Lokal.SkapaLokal(); break;
                    case 3: Lokal.TaBortLokal(); break;
                    case 0: Console.Clear(); Start(); break;
                }
            }

            int MenyVal(int max, bool canExit = false)
            {
            Redo:

                Console.WriteLine();
                if (canExit)
                    Console.WriteLine("Skriv \"avbryt\" för att avsluta.");
                Console.Write($"Menyval (1-{max}): ");
                
                string? line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    MethodRepository.PrintColor("Ogiltigt inmatning!", ConsoleColor.Red);
                    goto Redo;
                }

                line = line.ToLower();

                if (line == "avbryt" && canExit)
                {
                    Environment.Exit(0);
                    return 0;
                }

                if (!int.TryParse(line, out int val))
                {
                    MethodRepository.PrintColor("Ogiltigt inmatning!", ConsoleColor.Red);
                    goto Redo;
                }

                if (val > max)
                {
                    MethodRepository.PrintColor($"Menyvalet måste vara mellan 1-{max}", ConsoleColor.Red);
                    goto Redo;
                }

                switch(val)
                {
                    default: break;
                }

                return val;

            }

        }
    }
}
