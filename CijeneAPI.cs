using CijeneAPI.Importer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CijeneAPI {
	class CijeneAPI {
		List<CijeneImporter> Importers = new List<CijeneImporter>();


		public CijeneAPI() {
			Importers.Add(new Importer_Lidl());
		}

		public void FetchData(DateTime FetchDate) {
			HttpClient Cli = new HttpClient();


			foreach (CijeneImporter Imp in Importers) {
				Imp.FetchDocuments(Cli, FetchDate);
			}
		}
	}
}
