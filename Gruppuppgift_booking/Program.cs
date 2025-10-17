namespace Gruppuppgift_booking
{
    interface IBookable //Den abstrakta Bookable-klassen, som skall implementeras som returtyp vid bokning.
    { 
    
    }
    static class MethodRepository //En klass för att samla våra metoder.
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
    internal class Program
    {
        static void Main(string[] args)
        {
            int menuChoice;
            Console.WriteLine("Välkommen till bokningssystemet!");
            Console.WriteLine();
            Console.WriteLine("Mata in en siffra för att välja.");
            Console.WriteLine("1. Hantera bokningar.");
            Console.WriteLine("2. Hantera lokaler.");
            Int32.TryParse(Console.ReadLine(), out menuChoice);
            switch(menuChoice)
            {
                case 1:
                    Console.WriteLine("1. Skapa ny bokning.");
                    Console.WriteLine("2. Lista alla bokningar.");
                    Console.WriteLine("3. Uppdatera bokning.");
                    Console.WriteLine("4. Ta bort en bokning.");
                    Console.WriteLine("5. Lista bokningar för år:");
                    break;
                case 2:
                    Console.WriteLine("1. Lista alla salar.");
                    Console.WriteLine("2. Skapa ny lokal.");
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }

        }
    }
}
