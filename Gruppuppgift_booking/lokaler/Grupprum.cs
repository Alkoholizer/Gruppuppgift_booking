using Gruppuppgift_booking.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public class Grupprum : Lokal
    {
        public int GrupprumNummer;
        public bool Soffa;
        
        // Denna metod ska EJ användas när nya lokaler skapas av användaren!
        public Grupprum(LokalData data) : base(data)
        {
            GrupprumNummer = data.Nummer;
            Soffa = data.HarSoffa;

            Lokaler.Add(this);
        }

        public Grupprum(string namn, double area) : base(LokalTyp.Grupprum, namn, area)
        {
            
            Lokaler.Add(this);
        }

        public void GrupprumMaker()
        {
            Console.Clear();
            Console.WriteLine("Ange ett namn på grupprummet.");
            string namn = Console.ReadLine();
            MethodRepository.NullCheck(namn);
            //UniqueCheck(namn);

            Console.WriteLine("Ange antal sittplatser i grupprummet.");
            int.TryParse(Console.ReadLine(), out int platser);

            Console.WriteLine("Ange ett indexeringsnummer för grupprummet.");
            int.TryParse(Console.ReadLine(), out int grupprumNr);

            Console.WriteLine("Har grupprummet en soffa? Y/N?");
            string pick = Console.ReadLine();
            MethodRepository.NullCheck(pick);
            string _pick = pick.ToUpper();
            bool soffa;
            switch (_pick)
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
        }

    }
}
 