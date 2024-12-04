using Dtos;

namespace InventoryMicroservice.RepositoryService
{
    public interface IInventoryRepository
    {
        public GetInventoryResponse GetInventory();
        public BuyResponse Buy(Dtos.BuyRequest request);

    }
}
