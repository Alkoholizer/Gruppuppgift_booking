using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public class Sal : Lokal
    {
        public enum salNummer;
        public bool Projektor;
        public Sal (string Namn, int Platser, Enum salNummer, bool Projektor) : base(Namn, Platser)
        {
            salNummer = salNummer;
            Projektor = Projektor;
        }

    }
}
