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
            Console.WriteLine("Har salen en projektor? Y/N?");
            string pick = Console.ReadLine();
            MethodRepository.NullCheck(pick);
            string _pick = pick.ToUpper();
            bool projektor;
            switch (_pick)
            {
                case "Y":
                projektor = true;
                break;
                case "N":
                projektor = false;
                break;
                default:
                Console.WriteLine("Ogiltig input, ingen projektor sparades.");
                Console.ReadLine();
                projektor = false;
                break;
            }
            HarProjektor = projektor;

        }


    }
}