using Gruppuppgift_booking.filer;
using Gruppuppgift_booking.Methods;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

        noArea:
            Console.Write("\nAnge area i kvadratmeter: ");
            string? areaInput = Console.ReadLine();

            if (!double.TryParse(areaInput, out double area) || area <= 0)
            {
                MethodRepository.PrintColor("Ogiltigt värde för area, ange ett positivt nummer!", ConsoleColor.Red);
                goto noArea;
            }

        NoTyp:
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
                    MethodRepository.PrintColor("Ogiltig lokal typ!", ConsoleColor.Red);
                    goto NoTyp;
            }

            Lokaler.Add(lokalObj);
            MethodRepository.PrintColor(
                $"Lokal av typen: {lokalObj.Typ} med lokalnamn: {lokalObj.Namn} har nu skapats och sparats", ConsoleColor.Green);
            
            // Den här ska vara kvar!
            SparaLokaler();

            Program.ReturnFromMenu(MenyTyp.Lokaler);
        }

        public static void TaBortLokal()
        {
            Console.Clear();
            MethodRepository.PrintColor("===RADERING AV LOKALER===", ConsoleColor.Cyan);

            VisaLokaler(true, frånBooking: true);
        noID:
            Console.Write("\n Ange ID på lokalen du vill ta bort (eller tryck ENTER för att avbryta");
            string? idInput = Console.ReadLine();
            
            if (string.IsNullOrEmpty(idInput))
            {
                MethodRepository.PrintColor("Radering av lokal är AVBRUTEN", ConsoleColor.Yellow);
                Program.ReturnFromMenu(MenyTyp.Lokaler);
                return;
            }

            if (!int.TryParse(idInput, out int idToRemove))
            {
                MethodRepository.PrintColor("Ogiltig ID, Ange ett nummer.", ConsoleColor.Red);
                goto noID;
            }

            Lokal? lokalAttTaBort = Lokaler.FirstOrDefault(l => l.ID == idToRemove);

            if (lokalAttTaBort == null)
            {
                MethodRepository.PrintColor($"Lokal med ID: {idToRemove} hittades inte", ConsoleColor.Red);
                goto noID;
            }

            if (lokalAttTaBort != null)
            {
                MethodRepository.PrintColor(
                    $"Lokal med ID: {lokalAttTaBort.Namn} har en aktiv bokning. Du måste ta bort bokningen först ConsoleColor.Yellow");
                goto noID;
            }

            Lokaler.Remove(lokalAttTaBort);

            MethodRepository.PrintColor($"Lokal: {lokalAttTaBort.Namn} med ID: {lokalAttTaBort.ID} har tagits bort permanent");

            // Den här ska vara kvar!
            SparaLokaler();

            Program.ReturnFromMenu(MenyTyp.Lokaler);
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
    }

    public enum LokalTyp
    {
        Lokal = 1,
        Grupprum = 2,
        Sal = 3,
    }
}   