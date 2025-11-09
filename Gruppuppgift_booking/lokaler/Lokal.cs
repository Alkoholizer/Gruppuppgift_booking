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

        int Nummer,
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

            int number = 0;

            if (sal != null) number = sal.SalNummer;
            if (grupp != null) number = grupp.GrupprumNummer;

            LokalData lok = new
            (
                ID,

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
        }

        public static void TaBortLokal()
        {
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

    }

    public enum LokalTyp
    {
        Lokal = 1,
        Grupprum = 2,
        Sal = 3,
    }
}   