using Gruppuppgift_booking.filer;
using Gruppuppgift_booking.Methods;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public record LokalData
    (
        int ID,

        LokalTyp Typ,

        string Namn,
        double Area,

        bool HarSoffa,
        bool HarProjector
    );

    public class Lokal
    {
        public const string FILENAME = "lokaler.json";

        /// <summary>
        /// Den här körs bara när programmet startar. (Vi läser in lokal filer)
        /// </summary>
        public Lokal(LokalData data)
        {
            ID = data.ID;
            if (ID > IncrementID)
                IncrementID = ID;

            Namn = data.Namn;
            Typ = data.Typ;
            Area = data.Area;
        }

        /// <summary>
        /// Använd denna för att skapa en ny lokal i programmet.
        /// </summary>
        public Lokal(LokalTyp typ, string namn, double area)
        {
            IncrementID++;
            ID = IncrementID;

            Namn = namn;
            Typ = typ;
            Area = area;
        }
        private static int IncrementID;

        public readonly int ID;

        public string Namn { get; private set; }
        public LokalTyp Typ { get; private set; }
        public double Area { get; private set; }

        public Booking? Bokning { get; private set; }
        public void SetBokning(Booking? book)
        {
            Bokning = book;
        }

        
        public static readonly List<Lokal> Lokaler = [];



        public LokalData ToLokalData()
        {
            var grupp = this as Grupprum;
            var sal = this as Sal;

            LokalData lok = new
            (
                ID,

                Typ,
                Namn,
                Area,

				grupp != null && grupp.Soffa,

				sal != null && sal.HarProjektor


			);

            return lok;
        }

        
	
        public static void VisaLokaler(bool visaBokade, bool frånBooking = false)
        {
            if (!frånBooking)
            {
                Console.Clear();
                MethodRepository.PrintColor("===RUMSFÖRTECKNING===", ConsoleColor.Cyan);
            }
            else
            {
                MethodRepository.PrintColor("Lokaler", ConsoleColor.Cyan);
            }

            foreach(var lok in Lokaler)
            {
                if (lok.Bokning != null && !visaBokade)
                    continue;

                string txt = $"[{lok.ID}]: \"{lok.Namn}\"";
                if (lok.Bokning != null && visaBokade)
                    txt += $" (Bokad av: {lok.Bokning.CustomerName})";

                Console.WriteLine(txt);
            }

            if (!frånBooking)
                Program.ReturnFromMenu(MenyTyp.Lokaler);
        }

        public static void SkapaLokal()
        {
            Console.Clear();
            MethodRepository.PrintColor("===SKAPANDE AV LOKALER===", ConsoleColor.Cyan);

        NoName:
            Console.Write("Ange ett namn på lokalen: ");
            string? namn = Console.ReadLine();
            if (string.IsNullOrEmpty(namn))
            {
                MethodRepository.PrintColor("Ogiltigt namn!", ConsoleColor.Red);
                goto NoName;
            }

            Console.WriteLine("Ange antal sittplatser i lokalen.");
            int.TryParse(Console.ReadLine(), out int platser);

            Console.WriteLine("Vilken typ av lokal är det?");
            Console.WriteLine("1. Sal");
            Console.WriteLine("2. Grupprum");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                MethodRepository.PrintColor("Ogiltig lokal typ!", ConsoleColor.Red);
                goto NoTyp;
            }

            Lokal? lokalObj = null;

            switch(choice)
            {
                case 1:
                    Sal sal = new Sal(namn, area);
                    sal.SalMaker();
                    lokalObj = sal;
                    break;
                case 2:
                    Grupprum grup = new Grupprum(namn, area);
                    grup.GrupprumMaker();
                    lokalObj = grup;
                    break;
                default:
                    Console.WriteLine("Error: Ogiltigt val.");
                    break;
            }
            /* TODO: Skapa en ny lokal och spara den i "Lokaler" listan i denna klassen.
                Du måste också specificera om Lokalen är ett Grupprum eller en Sal!

                Till Exempel:
                    Välj lokalens namn här. Men om vi är ett grupprum, fråga användaren om lokalen har en soffa.
                    För detta kan du använda dig av Grupprum klassen och köra en metod i denna metod.
                    Ex: GrupprumMaker();
            
                Det behövs ej att ange lokalens lokaltyp, det körs automatiskt när lokalerna skapas.
                    Du kan kolla Grupprum och Sal konstruktors och se hur de fungerar.

                Grupprum och Sal har konstruktors som kan användas för att skapa dem.
                Ex:
                    Grupprum nyGrupp = new Grupprum(NAMN, AREA);
                    Sal nySal = new Sal(NAMN, AREA);
            

                GÖR NULL CHECKAR OCH FEL KOLLAR!

                Booking.cs klassen har exempel på hur du kan skriva koden här.
            */


            // Den här ska vara kvar!
            SparaLokaler();

            Program.ReturnFromMenu(MenyTyp.Lokaler);
        }

        public static void TaBortLokal()
        {
            Console.Clear();
            MethodRepository.PrintColor("===RADERING AV LOKALER===", ConsoleColor.Cyan);

            /* TODO: Välj en lokal att ta bort. Använd "Lokaler" listan i denna klassen.
                Tips: Alla lokaler har ett unikt ID, använd den!
                    I Booking klassen används en LINQ metod som heter "FirstOrDefault, kolla hur den används!

                Extra: Om en Lokal har en bokning, måste du också ta bort den bokningen!
                    När den bokningen är borttagen, kör Booking.SparaBokningar(); metoden.
            

                GÖR NULL CHECKAR OCH FEL KOLLAR!

                Booking.cs klassen har exempel på hur du kan skriva koden här.
            */


            // Den här ska vara kvar!
            SparaLokaler();
        }

        // Rör ej, den här är den enda funktionen som behövs för att spara lokaler!
        private static void SparaLokaler()
        {
            List<LokalData> lokDatas = [];

            foreach(var lok in Lokaler)
                lokDatas.Add(lok.ToLokalData());

            FilHantering.WriteJson
            (
                FILENAME, 
                lokDatas, 
                new System.Text.Json.JsonSerializerOptions()
                {
                    WriteIndented = true
                }
            );
        }
/*        private static bool UniqueCheck(string namn)
        {
            bool isUnique = true;
            foreach (var lok in Lokaler)
            {
                if(namn == Namn)
                {
                    isUnique = false;
                    break;
                }
            }
            return isUnique;
        }
*/

    }

    public enum LokalTyp
    {
        Lokal = 1,
        Grupprum = 2,
        Sal = 3,
    }
}   