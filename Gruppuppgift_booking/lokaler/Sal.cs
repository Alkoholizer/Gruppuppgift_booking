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
        public bool Projektor;
        public Sal(string namn, int platser, int salNummer, bool projektor) : base(namn, platser)
        {
            SalNummer = salNummer;
            Projektor = projektor;
        }
        public void SalMaker()
        {
            Console.Clear();
            Console.WriteLine("Ange ett namn på salen.");
            string namn = Console.ReadLine();

            Console.WriteLine("Ange antal sittplatser i salen.");
            int.TryParse(Console.ReadLine(), out int platser);

            Console.WriteLine("Ange ett indexeringsnummer för salen.");
            int.TryParse(Console.ReadLine(), out int salNr);

            Console.WriteLine("Har salen en projektor? Y/N?");
            string pick = Console.ReadLine();
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
            Sal S = new Sal(namn, platser, salNr, projektor);
            sparaSalar.Add(S);
        }
    }
}
