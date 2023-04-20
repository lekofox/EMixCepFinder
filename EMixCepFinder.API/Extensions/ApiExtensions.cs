using EmixCepFinder.Service;
using EMixCepFinder.Domain.CepFinderClient;
using EMixCepFinder.Domain.Service;
using Refit;

namespace EMixCepFinder.API.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddViaCepService(this IServiceCollection services, IConfiguration configuration)
        {
            var apiBaseAddress = configuration["Services:ViaCep:ApiBaseAddress"];
            services.AddApiClient<IViaCepClient>(apiBaseAddress);
            services.AddScoped<IViaCepService, ViaCepService>();
            return services;
        }

        private static void AddApiClient<T>(this IServiceCollection services, string baseAddress)
             where T : class
        {

            services.AddRefitClient<T>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        }
    }
}
