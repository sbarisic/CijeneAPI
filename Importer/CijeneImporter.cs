using CijeneAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CijeneAPI.Importer {
	abstract class CijeneImporter {
		public string StoreName;
		public string BaseLink;

		protected List<CijeneAPIEntry> Entries = new List<CijeneAPIEntry>();

		public CijeneImporter(string StoreName, string BaseLink) {
			this.StoreName = StoreName;
			this.BaseLink = BaseLink;
		}

		public abstract void FetchDocuments(HttpClient Cli, DateTime DT);
	}
}
