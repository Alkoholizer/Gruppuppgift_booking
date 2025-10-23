using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public class Grupprum : Lokal
    {
        public enum grupprumNummer;
        public bool Soffa;
        public Grupprum (string Namn, int Platser, Enum grupprumNummer, bool Soffa) : base (Namn, Platser)
        {
            grupprumNummer = grupprumNummer;
            Soffa = Soffa;
        }
    }
}
