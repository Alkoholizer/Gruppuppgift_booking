using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public class Sal : Lokal
    {
        public int salNummer;
        public bool Projektor;
        public Sal(string Namn, int Platser, int salNummer, bool Projektor) : base(Namn, Platser)
        {
            salNummer = salNummer;
            Projektor = Projektor;
        }
        public void salMaker()
        {
            Console.Clear();
            Console.WriteLine("Ange ett namn på salen.");
            string namn = Console.ReadLine();
            Console.WriteLine("Ange antal sittplatser i salen.");
            int platser;
            Int32.TryParse(Console.ReadLine(), out platser);
            Console.WriteLine("Ange ett indexeringsnummer för salen.");
            int salNr;
            Int32.TryParse(Console.ReadLine(), out salNr);
            Console.WriteLine("Har salen en projektor? Y/N?");
            string pick = Console.ReadLine();
            string Pick = pick.ToUpper();
            bool projektor;
            switch (Pick)
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
            Grupprum GR = new Grupprum(namn, platser, salNr, projektor);

        }
    }
}
