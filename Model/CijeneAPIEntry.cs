using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CijeneAPI.Model {
	public class CijeneAPIEntry {
		public string Store;
		public string Location;
		public string Address;

		public string Naziv {
			get; set;
		}

		public string Sifra {
			get; set;
		}

		public string NetoKolicina {
			get; set;
		}

		public string JedinicaMjere {
			get; set;
		}

		public string Pakiranje {
			get; set;
		}

		public string Marka {
			get; set;
		}

		public string MaloprodajnaCijena {
			get; set;
		}

		public string CijenaZaJedinicuMjere {
			get; set;
		}

		public string Barkod {
			get; set;
		}

		public string KategorijaProizvoda {
			get; set;
		}

		public string SidrenaCijena {
			get; set;
		}

		public override string ToString() {
			return string.Format("[{0} {1}] - {2}, {3} EUR", Store, Location, Naziv, MaloprodajnaCijena);
		}
	}
}
