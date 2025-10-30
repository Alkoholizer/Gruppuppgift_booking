using Gruppuppgift_booking.Methods;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public record LokalData
    (
        LokalTyp Typ,

        string Namn,
        double Area,

        int Nummer,
        bool HarSoffa,
        bool HarProjector
    );

    public class Lokal
    {
        private static List<Booking> bokningar = [];
        public static readonly List<Lokal> lokaler = [];

        public Lokal(LokalData data)
        {
            Namn = data.Namn;
            Typ = data.Typ;
        }

        public string Namn;
        public int Platser;
        public Lokal(string namn, int platser)
        {
            Namn = namn;
            Platser = platser;
        }
        public List<Grupprum> sparaGrupprum = new List<Grupprum>();
        public List<Sal> sparaSalar = new List<Sal>();
    }
}         
    


