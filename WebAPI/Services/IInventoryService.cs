using Dtos;

namespace WebAPI.Services
{
    public interface IInventoryService
    {
        public Task<GetInventoryResponse> GetAll();
        public Task<BuyResponse> Buy(Dtos.BuyRequest buyRequest);

    }
}
