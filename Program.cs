namespace CijeneAPI {
	internal class Program {
		static void Main(string[] args) {
			CijeneAPI CAPI = new CijeneAPI();

			CAPI.FetchData(DateTime.Today.AddDays(-1));

			Console.WriteLine("Done!");
			Console.ReadLine();
		}
	}
}
