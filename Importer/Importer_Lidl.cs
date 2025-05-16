using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CijeneAPI.Importer {
	class Importer_Lidl : CijeneImporter {

		public Importer_Lidl() : base("Lidl", "https://tvrtka.lidl.hr/cijene") {

		}

		public override void FetchDocuments(HttpClient Cli, DateTime DT) {
			Entries.Clear();

			string DnlStr = Cli.GetStringAsync(BaseLink).GetAwaiter().GetResult();
			string DateStr = DT.ToString("dd_MM_yyyy");

			List<string> FileResults = new List<string>();

			string[] Lines = DnlStr.Split("\n").ToArray();
			foreach (var L in Lines) {
				if (Util.Contains(L, "https://tvrtka.lidl.hr/content/download", ".zip", out string Lnk)) {

					if (Lnk.Contains(DateStr + ".zip"))
						FileResults.Add(Lnk);
				}
			}

			foreach (string FR in FileResults) {
				Console.WriteLine(">> {0}", FR);

				using (MemoryStream ZipStream = new MemoryStream(Cli.GetByteArrayAsync(FR).GetAwaiter().GetResult()))
					UnpackZip(ZipStream, DT);
			}
		}

		void UnpackZip(Stream ZipStream, DateTime DT) {
			using (ZipArchive Zip = new ZipArchive(ZipStream, ZipArchiveMode.Read)) {
				foreach (var E in Zip.Entries) {
					Console.WriteLine(">>>> {0}", E.FullName);
					string CSV_Contents = "";

					using (Stream S = E.Open()) {
						CSV_Contents = Encoding.Unicode.GetString(S.ToByteArray());

						ParseCSV(E.FullName, CSV_Contents, DT);
					}
				}
			}
		}

		void ParseCSV(string CSVName, string CSVSrc, DateTime DT) {

		}
	}
}
