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
        LokalTyp Typ,

        string Namn,
        double Area,

        int Nummer,
        bool HarSoffa,
        bool HarProjector
    );

    public class Lokal
    {
        public Lokal(LokalData data)
        {
            IncrementID++;
            ID = IncrementID;

            Namn = data.Namn;
            Typ = data.Typ;
            Area = data.Area;
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

            int number = 0;

            if (sal != null) number = sal.SalNummer;
            if (grupp != null) number = grupp.GrupprumNummer;

            LokalData lok = new
            (
                Typ,
                Namn,
                Area,
                number,

				grupp != null && grupp.Soffa,

				sal != null && sal.HarProjektor


			);

            return lok;
        }

        
	
        public static void VisaLokaler(bool visaBokade)
        {
            int id = 0;
            foreach(var lok in Lokaler)
            {
                if (lok.Bokning != null && !visaBokade)
                    continue;

                string txt = $"[{id}]: \"{lok.Namn}\"";
                if (visaBokade)
                    txt += " (Bokad!)";

                Console.WriteLine(txt);
                id++;
            }
        }

        public static void SkapaLokal()
        {
            /* TODO: Skapa en ny lokal och spara den i "Lokaler" listan i denna klassen.
                Du måste också specificera om Lokalen är ett Grupprum eller en Sal!

                GÖR NULL CHECKAR OCH FEL KOLLAR!

                Till Exempel:
                    Välj lokalens namn här. Men om vi är ett grupprum, fråga användaren om lokalen har en soffa

            */


            // Den här ska vara kvar!
            SparaLokaler();
        }

        public static void TaBortLokal()
        {
            /* TODO: Välj en lokal att ta bort. Använd "Lokaler" listan i denna klassen.

                GÖR NULL CHECKAR OCH FEL KOLLAR!

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
                "lokaler.json", 
                lokDatas, 
                new System.Text.Json.JsonSerializerOptions()
                {
                    WriteIndented = true
                }
            );
        }


<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
    }

    public enum LokalTyp
    {
        Lokal = 1,
        Grupprum = 2,
        Sal = 3,
    }
}   