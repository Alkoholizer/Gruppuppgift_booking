using Gruppuppgift_booking.filer;
using Gruppuppgift_booking.Methods;

namespace Gruppuppgift_booking
{
    interface IBookable //Den abstrakta Bookable-klassen, som skall implementeras som returtyp vid bokning.
    {
        int? NewBooking(string costumerName, DateTime startTime, DateTime endTime);

        bool CancelBooking(int bookingId);

        bool CheckBookingStatus(int bookingId);
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            FilHantering.Init();

            Start();
        }

        public static void Start(int menuIndex = -1)
        {
            int val = menuIndex;
            if (val >= 0)
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
                                "\n5: Lista bokningar för år" +
                                "\n0: Gå tillbaka");

                switch (MenyVal(5))
                {
                    case 1: ManageBookings(); break;
                    case 2: HandleRooms(); break;
                    case 0: Start(); Console.Clear(); break;
                }
            }
            void HandleRooms()
            {
                Console.WriteLine("1: Lista alla lokaler " +
                                "\n2: Skapa ny lokal");
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
