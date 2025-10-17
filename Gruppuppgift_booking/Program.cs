namespace Gruppuppgift_booking
{
    interface IBookable //Den abstrakta Bookable-klassen, som skall implementeras som returtyp vid bokning.
    { 
    
    }
    class Lokal //Huvudklassen för lokaler, med delade egenskaper
    { 
    
    }
    class Grupprum : Lokal //Underklass som ärver från Lokal-klassen, med unika egenskaper för just Grupprum
    { 
    
    }
    class Sal : Lokal //Samma sak, fast med egenskaper för salar.
    {

    }
    static class methodRepository //En klass för att samla våra metoder.
    { 
    
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
