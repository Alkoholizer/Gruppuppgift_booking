using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.filer
{
	public static class FilHantering
	{
		private static string StandardFilPath = "";

		public static void Init()
		{
			// Om vi är på debug versionen, använg lokala mappar i våra dokument.
			#if DEBUG
				StandardFilPath = Path.Combine(Environment.GetFolderPath
					(Environment.SpecialFolder.MyDocuments), "Net-Utvecklare", "Booking");
			#else
				FilPath = AppContext.BaseDirectory;
			#endif
		}

		/// <summary>
		/// Läs in en fil från en specificerad path. Syntax: LäsFil("Fil.txt", ""Mapp1", "Mapp2")
		/// </summary>
		/// <param name="filNamn">Namnet på filen, måste ha fil extension med!"</param>
		/// <param name="filPaths">Pathen till filen (Mappar). Använder sig av specifika plater med hjälp av StandardFilPath</param>
		/// <returns>Alla rader av text från filen.</returns>
		public static string[] LäsFil(string filNamn, params string[] filPaths)
		{
			string path = Path.Combine(StandardFilPath, Path.Combine(filPaths));
			if (Directory.Exists(path))
				Directory.CreateDirectory(path);

			string pathTillFil = Path.Combine(path, filNamn);

			if (File.Exists(pathTillFil))
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
		/// <param name="filPaths">Pathen till filen (Mappar). Använder sig av specifika plater med hjälp av StandardFilPath</param>
		public static void SparaFil(string[] data, string filNamn, params string[] filPaths)
		{
			string path = Path.Combine(StandardFilPath, Path.Combine(filPaths));
			if (Directory.Exists(path))
				Directory.CreateDirectory(path);

			string pathTillFil = Path.Combine(path, filNamn);

			File.WriteAllLines(pathTillFil, data);
		}
	}
}
