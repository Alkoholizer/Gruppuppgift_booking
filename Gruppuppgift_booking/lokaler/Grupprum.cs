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
        public bool Soffa;
        
        // Denna metod ska EJ användas när nya lokaler skapas av användaren!
        public Grupprum(LokalData data) : base(data)
        {
            Soffa = data.HarSoffa;

            Lokaler.Add(this);
        }

        public Grupprum(string namn, double area) : base(LokalTyp.Grupprum, namn, area)
        {
            
            Lokaler.Add(this);
        }

        public void GrupprumMaker()
        {
        Redo:
            Console.WriteLine("Har grupprummet en soffa? (Y/N): ");
            string? pick = Console.ReadLine();
            
            if (string.IsNullOrEmpty(pick))
                goto Redo;

            switch (pick.ToUpper())
            {
                case "Y":
                    Soffa = true;
                    break;
                case "N":
                    Soffa = false;
                    break;
                default:
                    goto Redo;
            }
        }

    }
}
 