using Gruppuppgift_booking.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public class Sal : Lokal
    {
        public bool HarProjektor;

        // Denna metod ska EJ användas när nya lokaler skapas av användaren!
        public Sal(LokalData data) : base(data)
        {
            HarProjektor = data.HarProjector;

            Lokaler.Add(this);
        }

        public Sal(string namn, double area) : base(LokalTyp.Sal, namn, area)
        {

            Lokaler.Add(this);
        }

        public void SalMaker()
        {
        Redo:
            Console.WriteLine("Har salen en projektor? Y/N?");
            string? pick = Console.ReadLine();
            
            if (string.IsNullOrEmpty(pick))
                goto Redo;

            switch (pick.ToUpper())
            {
                case "Y":
                    HarProjektor = true;
                    break;
                case "N":
                    HarProjektor = false;
                    break;
                default:
                    goto Redo;
            }


        }


    }
}