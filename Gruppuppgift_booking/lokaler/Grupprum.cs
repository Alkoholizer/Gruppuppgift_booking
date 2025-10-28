using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public class Grupprum : Lokal
    {
        public int grupprumNummer;
        public bool Soffa;
        public Grupprum(string Namn, int Platser, int grupprumNummer, bool Soffa) : base(Namn, Platser)
        {
            grupprumNummer = grupprumNummer;
            Soffa = Soffa;
        }
        public void grupprumMaker()
        {
            Console.Clear();
            Console.WriteLine("Ange ett namn på grupprummet.");
            string namn = Console.ReadLine();
            Console.WriteLine("Ange antal sittplatser i grupprummet.");
            int platser;
            Int32.TryParse(Console.ReadLine(), out platser);
            Console.WriteLine("Ange ett indexeringsnummer för grupprummet.");
            int grupprumNr;
            Int32.TryParse(Console.ReadLine(), out grupprumNr);
            Console.WriteLine("Har grupprummet en soffa? Y/N?");
            string pick = Console.ReadLine();
            string Pick = pick.ToUpper();
            bool soffa;
            switch (Pick)
            {
                case "Y":
                    soffa = true;
                    break;
                case "N":
                    soffa = false;
                    break;
                default:
                    Console.WriteLine("Ogiltig input, ingen soffa sparades.");
                    Console.ReadLine();
                    soffa = false;
                    break;

            }
            Grupprum GR = new Grupprum(namn, platser, grupprumNr, soffa);
            Lokal.sparaGrupprum.add(GR);
        }
    }
}
