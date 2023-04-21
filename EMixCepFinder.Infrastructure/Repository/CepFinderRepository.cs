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

        public async Task<AddressInfo> Select(string postalCode)
        {
            return await GetAddressByPostalCode(postalCode);
        }

        public async Task<List<AddressInfo>> SelectByState(string state)
        {
            return await GetAddressInfosByState(state);
        }

        private async Task<List<AddressInfo>> GetAddressInfosByState(string state) =>
            await _context.AddressInfo.Where(ai => ai.State == state).ToListAsync();


        private async Task<AddressInfo> GetAddressByPostalCode(string postalCode) =>
            await _context.AddressInfo.FirstOrDefaultAsync(ai => ai.PostalCode == postalCode);
    }
}
