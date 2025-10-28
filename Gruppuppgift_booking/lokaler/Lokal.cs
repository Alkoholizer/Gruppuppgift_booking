using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public class Lokal
    {
        public string Namn;
        public int Platser;
        public Lokal (string Namn, int Platser)
        {
            Namn = Namn;
            Platser = Platser;
        }
        public List<Grupprum> sparaGrupprum = new List<Grupprum>();
        public List<Sal> sparaSalar = new List<Sal>();
    }
}
