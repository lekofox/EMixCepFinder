using EMixCepFinder.Domain.Model;

namespace EMixCepFinder.Domain.Service
{
    public interface ICepFinderService
    {
        public Task<AddressInfo> GetAddressInfo(string cep);
    }
}
