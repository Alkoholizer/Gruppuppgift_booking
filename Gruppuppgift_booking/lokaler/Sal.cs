using Gruppuppgift_booking.filer;
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
        public int SalNummer;
        public bool HarProjektor;
        public Sal(LokalData data) : base(data)
        {
            SalNummer = data.Nummer;
            HarProjektor = data.HarProjector;

            Lokal.Lokaler.Add(this);
        }
        public void SalMaker()
        {
            Console.Clear();
            Console.WriteLine("Ange ett namn på salen.");
            string namn = Console.ReadLine();
<<<<<<< Updated upstream
            MethodRepository.NullCheck(namn);
            UniqueCheck(namn);
=======
            
			MethodRepository.NullCheck(namn);
>>>>>>> Stashed changes

            Console.WriteLine("Ange antal sittplatser i salen.");
            int.TryParse(Console.ReadLine(), out int platser);

            Console.WriteLine("Ange ett indexeringsnummer för salen.");
            int.TryParse(Console.ReadLine(), out int salNr);

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


        }


    }
}