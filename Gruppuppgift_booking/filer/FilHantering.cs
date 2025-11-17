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
		public static string StandardFilPath { get; private set; } = AppContext.BaseDirectory;

		/// <summary>
		/// Läs in en fil från en specificerad path. Syntax: LäsFil("Fil.txt", "Mapp1", "Mapp2")
		/// </summary>
		/// <param name="filNamn">Namnet på filen, måste ha fil extension med!"</param>
		/// <param name="filPaths">Pathen till filen (Mappar). Använder sig av specifika platser med hjälp av StandardFilPath</param>
		/// <returns>Alla rader av text från filen.</returns>
		public static string[] LäsFil(string filNamn)
		{
			string path = Path.Combine(StandardFilPath, filNamn);

			if (!File.Exists(path))
			{
				throw new FileNotFoundException($"Kunde ej hitta filen \"{filNamn}\" på platsen: {path}");
			}

			return File.ReadAllLines(path);
		}

		/// <summary>
		/// Sparar all data till en fil. Syntax: SparaFil(DinData, "Fil.txt", "Mapp1", "Mapp2")
		/// </summary>
		/// <param name="data">Din data i formet som en string array.</param>
		/// <param name="filNamn">Namnet på filen du vill skapa.</param>
		/// <param name="filPaths">Pathen till filen (Mappar). Använder sig av specifika platser med hjälp av StandardFilPath</param>
		public static void SparaFil(string[] data, string filNamn)
		{
			string path = Path.Combine(StandardFilPath, filNamn);
			File.WriteAllLines(path, data);
		}


        private readonly static JsonSerializerOptions JSON_ReadSerialized = new()
        {

        };
        public static bool ReadJson<T>(out T? outer, out Exception? exc, params string[] paths)
        {
			string path = Path.Combine(StandardFilPath, Path.Combine(paths));
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
