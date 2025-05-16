using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CijeneAPI {
	static class Util {
		public static bool Contains(string Str, string Begin, string End, out string FoundStr) {
			FoundStr = "";

			int BeginIdx = Str.IndexOf(Begin);

			if (BeginIdx >= 0) {
				int EndIdx = Str.IndexOf(End, BeginIdx + Begin.Length);

				if (BeginIdx >= 0 && EndIdx >= 0 && EndIdx > BeginIdx) {
					FoundStr = Str.Substring(BeginIdx, (EndIdx - BeginIdx) + End.Length);
					return true;
				}
			}

			return false;
		}

		public static byte[] ToByteArray(this Stream S) {
			using (MemoryStream MS = new MemoryStream()) {
				S.CopyTo(MS);
				return MS.ToArray();
			}
		}
	}
}
