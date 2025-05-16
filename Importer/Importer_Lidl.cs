using CijeneAPI.Model;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TinyCsvParser;
using TinyCsvParser.Mapping;

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

					//string CSV_Contents = "";
					//byte[] CSV_Bytes = E.Open().ToByteArray();
					//CSV_Contents = Encoding.ASCII.GetString(CSV_Bytes);

					//CSV_Contents = Encoding.Default.GetString(CSV_Bytes);

					ParseCSV(E.FullName, E.Open(), DT);

				}
			}
		}

		void ParseCSV(string CSVName, Stream CSVSrc, DateTime DT) {
			CsvParserOptions CsvParserOpt = new CsvParserOptions(true, ',');
			CsvLidlMapping LidlMapping = new CsvLidlMapping();
			CsvParser<CijeneAPIEntry> CsvParser = new CsvParser<CijeneAPIEntry>(CsvParserOpt, LidlMapping);

			string[] NameTokens = CSVName.Split("_").ToArray();
			CsvMappingResult<CijeneAPIEntry>[] MappingResult = CsvParser.ReadFromStream(CSVSrc, Encoding.ASCII, true).ToArray();

			foreach (CsvMappingResult<CijeneAPIEntry> MR in MappingResult) {
				CijeneAPIEntry Entry = MR.Result;
				Entry.Store = "Lidl";
				Entry.Location = NameTokens[1];

				string[] AddrTokens = new string[NameTokens.Length - 4];
				Array.Copy(NameTokens, 2, AddrTokens, 0, AddrTokens.Length);

				Entry.Address = string.Join(' ', AddrTokens);
				Entries.Add(Entry);
			}
		}
	}

	class CsvLidlMapping : CsvMapping<CijeneAPIEntry> {
		public CsvLidlMapping()
			: base() {

			int Cnt = 0;
			MapProperty(Cnt++, x => x.Naziv);
			MapProperty(Cnt++, x => x.Sifra);
			MapProperty(Cnt++, x => x.NetoKolicina);
			MapProperty(Cnt++, x => x.JedinicaMjere);
			MapProperty(Cnt++, x => x.Pakiranje);
			MapProperty(Cnt++, x => x.Marka);
			MapProperty(Cnt++, x => x.MaloprodajnaCijena);
			MapProperty(Cnt++, x => x.CijenaZaJedinicuMjere);
			MapProperty(Cnt++, x => x.Barkod);
			MapProperty(Cnt++, x => x.KategorijaProizvoda);
			MapProperty(Cnt++, x => x.SidrenaCijena);
		}
	}
}
