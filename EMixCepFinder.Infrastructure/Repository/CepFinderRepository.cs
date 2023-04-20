using EMixCepFinder.Domain.Model;
using EMixCepFinder.Domain.Repository;
using EMixCepFinder.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;


namespace EMixCepFinder.Infrastructure.Repository
{
    public class CepFinderRepository : ICepFinderRepository
    {
        private readonly AddressInfoContext _context;

        public CepFinderRepository(AddressInfoContext context)
        {
            _context = context;
        }

        public async Task Create(AddressInfo addressInfo)
        {
            _context.Add(addressInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<AddressInfo> Select(string cep)
        {
            return await GetAddressByPostalCode(cep);
        }

        private async Task<AddressInfo> GetAddressByPostalCode(string cep) =>
            await _context.AddressInfo.FirstOrDefaultAsync(ai => ai.PostalCode == cep);
    }
}
