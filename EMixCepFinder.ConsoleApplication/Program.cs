using EMixCepFinder.Domain.Model;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;

namespace EMixCepFinder.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to EMixCepFinder!");
            Console.WriteLine("How would you like to retrieve the address information?");
            Console.WriteLine("1. By CEP");
            Console.WriteLine("2. By State");
            Console.Write("Enter your choice (1 or 2): ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                await RetrieveByCep();
            }
            else if (choice == "2")
            {
                await RetrieveByState();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        private static async Task RetrieveByCep()
        {
            Console.Write("Enter the CEP: ");
            var cep = Console.ReadLine();
            var httpClient = new HttpClient();
            var baseAddress = "https://localhost:7139";
            var url = $"{baseAddress}/addressinfo?cep={cep}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var addressInfo = JsonConvert.DeserializeObject<AddressInfo>(content);
                Console.WriteLine($"Address: {addressInfo.Street}");
                Console.WriteLine($"City: {addressInfo.City} - {addressInfo.State}");
                Console.WriteLine($"CEP: {addressInfo.PostalCode}");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Error retrieving address information for CEP {cep}.");
            }
        }

        private static async Task RetrieveByState()
        {
            Console.Write("Enter the state initials (e.g. SP): ");
            var state = Console.ReadLine();

            var httpClient = new HttpClient();
            var baseAddress = "https://localhost:7139"; // Replace with the appropriate base URL for the API
            var url = $"{baseAddress}/addressinfo/state/state={state}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var addressInfos = JsonConvert.DeserializeObject<AddressInfo[]>(content);
                Console.WriteLine($"Address information for state {state}:");
                foreach (var addressInfo in addressInfos)
                {
                    Console.WriteLine($"Address: {addressInfo.Street}");
                    Console.WriteLine($"City: {addressInfo.City} - {addressInfo.State}");
                    Console.WriteLine($"CEP: {addressInfo.PostalCode}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Error retrieving address information for state {state}.");
            }
        }
    }
}