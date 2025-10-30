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
        public Lokal(string namn, int platser)
        {
            Namn = namn;
            Platser = platser;
        }
        public List<Grupprum> sparaGrupprum = new List<Grupprum>();
        public List<Sal> sparaSalar = new List<Sal>();
        public void UniqueCheck(string input)
        {
            foreach (Sal sal in sparaSalar)
            {
                if (input == Namn)
                {
                    Console.WriteLine("Fel, en lokal måste ha ett unikt namn.");
                    Console.ReadLine();
                    break;
                }
            }
            foreach(Grupprum grupprum in sparaGrupprum)
            {
                if(input == Namn)
                {
                    Console.WriteLine("Fel, en lokal måste ha ett unikt namn.");
                    Console.ReadLine();
                    break;                    
                }
            }
        }
    }
}