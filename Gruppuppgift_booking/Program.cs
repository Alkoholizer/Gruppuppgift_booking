using Gruppuppgift_booking.filer;

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

            Console.WriteLine("Välkommen till bokningssystemet!");
            Console.WriteLine();
            Console.WriteLine("Mata in en siffra för att välja.");
            Console.WriteLine("1. Hantera bokningar.");
            Console.WriteLine("2. Hantera lokaler.");

            switch(MenyVal(2))
            {
                case 1: ManageBookings(); break;
                case 2:
                    Console.WriteLine("1. Lista alla salar.");
                    Console.WriteLine("2. Skapa ny lokal.");
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }

            void ManageBookings()
            {
                Console.WriteLine("1: Skapa ny bokning " +
                                "\n2: Lista alla bokningar " +
                                "\n3: Uppdatera bokning " +
                                "\n4: Ta bort en bokning " +
                                "\n5: Lista bokningar för år");
                int val = MenyVal(5);
            }
            void HandleRooms()
            {

            }

            int MenyVal(int max)
            {
                Console.Write($"Menyval (1-{max}): ");
            Redo:
                if (!int.TryParse(Console.ReadLine(), out int val))
                {
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
