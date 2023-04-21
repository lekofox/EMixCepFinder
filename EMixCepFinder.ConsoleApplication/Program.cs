using EMixCepFinder.Domain.Model;
using Newtonsoft.Json;
using System.Net;

namespace EMixCepFinder.ConsoleApp
{
    class Program
    {
        const string BASEADDRESS = "https://localhost:7139/api/v1/CepFinder";
        static async Task Main(string[] args)
        {
            //Garantir que API está em execução antes de rodar a console app.
            Thread.Sleep(5000);
            Console.WriteLine("Welcome to EMixCepFinder!");

            while (true)
            {
                Console.WriteLine("How would you like to retrieve the address information?");
                Console.WriteLine("1. By CEP");
                Console.WriteLine("2. By State");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice (1, 2 or 0): ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    await RetrieveByCep();
                }
                else if (choice == "2")
                {
                    await RetrieveByState();
                }
                else if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        private static async Task RetrieveByCep()
        {
            Console.Write("Enter the CEP: ");
            var cep = Console.ReadLine();
            var httpClient = new HttpClient();
            var url = $"{BASEADDRESS}/{cep}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var addressInfo = JsonConvert.DeserializeObject<AddressInfo>(content);
                var result = new
                {
                    AddressInfos = addressInfo
                };

                var json = JsonConvert.SerializeObject(result, Formatting.Indented);

                Console.WriteLine(json);
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

            var url = $"{BASEADDRESS}/state/{state}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var addressInfos = JsonConvert.DeserializeObject<AddressInfo[]>(content);

                var result = new
                {
                    State = state,
                    AddressInfos = addressInfos
                };

                var json = JsonConvert.SerializeObject(result, Formatting.Indented);

                Console.WriteLine(json);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                Console.WriteLine("State does not have any postal code in our database, please select a postal code from this state and try again later");
            }
            else
            {
                Console.WriteLine($"Error retrieving address information for state {state}.");
            }
            Console.ReadLine();
        }
    }
}
