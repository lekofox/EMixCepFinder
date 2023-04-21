using EmixCepFinder.Service;
using EMixCepFinder.Domain.CepFinderClient;
using EMixCepFinder.Domain.Service;
using Refit;

namespace EMixCepFinder.API.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring API services.
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Adds the ViaCep service to the service collection using the provided configuration.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="configuration">The configuration containing the ViaCep API base address.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddViaCepService(this IServiceCollection services, IConfiguration configuration)
        {
            var apiBaseAddress = configuration["Services:ViaCep:ApiBaseAddress"];
            services.AddApiClient<IViaCepClient>(apiBaseAddress);
            services.AddScoped<IViaCepService, ViaCepService>();
            return services;
        }

        /// <summary>
        /// Adds an API client to the service collection with the specified base address.
        /// </summary>
        /// <typeparam name="T">The type of API client to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the API client to.</param>
        /// <param name="baseAddress">The base address of the API.</param>
        private static void AddApiClient<T>(this IServiceCollection services, string baseAddress)
            where T : class
        {
            services.AddRefitClient<T>()
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        }
    }
}