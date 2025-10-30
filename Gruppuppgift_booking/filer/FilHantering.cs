using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.filer
{
	public static class FilHantering
	{
		private static string StandardFilPath = "";

		public static void Init()
		{
			// Om vi är på debug versionen, använd lokala mappar i våra dokument.
			#if DEBUG
				StandardFilPath = Path.Combine(Environment.GetFolderPath
					(Environment.SpecialFolder.MyDocuments), "Net-Utvecklare", "Booking");
			#else
				FilPath = AppContext.BaseDirectory;
			#endif
		}

		/// <summary>
		/// Läs in en fil från en specificerad path. Syntax: LäsFil("Fil.txt", "Mapp1", "Mapp2")
		/// </summary>
		/// <param name="filNamn">Namnet på filen, måste ha fil extension med!"</param>
		/// <param name="filPaths">Pathen till filen (Mappar). Använder sig av specifika platser med hjälp av StandardFilPath</param>
		/// <returns>Alla rader av text från filen.</returns>
		public static string[] LäsFil(string filNamn, params string[] filPaths)
		{
			string path = Path.Combine(StandardFilPath, Path.Combine(filPaths));
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			string pathTillFil = Path.Combine(path, filNamn);

			if (!File.Exists(pathTillFil))
			{
				throw new FileNotFoundException($"Kunde ej hitta filen \"{filNamn}\" på platsen: {path}");
			}

			return File.ReadAllLines(pathTillFil);
		}

		/// <summary>
		/// Sparar all data till en fil. Syntax: SparaFil(DinData, "Fil.txt", "Mapp1", "Mapp2")
		/// </summary>
		/// <param name="data">Din data i formet som en string array.</param>
		/// <param name="filNamn">Namnet på filen du vill skapa.</param>
		/// <param name="filPaths">Pathen till filen (Mappar). Använder sig av specifika platser med hjälp av StandardFilPath</param>
		public static void SparaFil(string[] data, string filNamn, params string[] filPaths)
		{
			string path = Path.Combine(StandardFilPath, Path.Combine(filPaths));
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			string pathTillFil = Path.Combine(path, filNamn);

			File.WriteAllLines(pathTillFil, data);
		}


        private readonly static JsonSerializerOptions JSON_ReadSerialized = new()
        {

        };
        public static bool ReadJson<T>(out T? outer, out Exception? exc, params string[] paths)
        {
            string path = Path.Combine(paths);
            try
            {
                StreamReader read = new(path);
                outer = JsonSerializer.Deserialize<T>(read.ReadToEnd(), JSON_ReadSerialized);
                read.Close(); // Close read to open permissions to the file again.
                exc = null;
                return true;
            }
            catch (Exception e)
            {
                outer = default;
                exc = e;
                return false;
            }
        }
        public static void WriteJson<T>(string path, T rec, JsonSerializerOptions options)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(rec, options));
        }



    }
}
