using EMixCepFinder.Domain.Model;

namespace EMixCepFinder.Domain.Repository
{
    public interface ICepFinderRepository
    {
        public Task Create(AddressInfo addressInfo);

        public Task<AddressInfo> Select(string cep);
    }
}
