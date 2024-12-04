using Dtos;
using Grpc.Core;
using InventoryMicroservice.RepositoryService;
using static InventoryMicroservice.inventoryproto;

namespace InventoryMicroservice.Services
{
    public class InventoryService : inventoryproto.inventoryprotoBase
    {
        private static IInventoryRepository _inventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public override Task<GetInventoryGrpcResponse> GetAll(GetInventoryGrpcRequest request, ServerCallContext context)
        {
            GetInventoryResponse getInventoryResponse = _inventoryRepository.GetInventory();

            GetInventoryGrpcResponse grpcResponse = new GetInventoryGrpcResponse();

            foreach(Inventory inventory in getInventoryResponse.inventory)
            {
                inventoryItem inventoryItem = new inventoryItem();
                inventoryItem.Id = inventory.id;
                inventoryItem.Item = inventory.item;
                inventoryItem.ItemCount = inventory.item_count;
                inventoryItem.Price = inventory.price;
                grpcResponse.Inventory.Add(inventoryItem);
            }

            return Task.FromResult(grpcResponse);
        }

        public override Task<BuyReply> Buy(BuyRequest request, ServerCallContext context)
        {
            Dtos.BuyRequest buyRequest = new Dtos.BuyRequest();
            buyRequest.itemId = request.Itemid;
            buyRequest.quantity = request.Quantity;
            buyRequest.username = request.Username;

            BuyResponse response = _inventoryRepository.Buy(buyRequest);

            BuyReply reply = new BuyReply()
            {
                Code = response.statusCode.code,
                Message=response.statusCode.message
            };
            return Task.FromResult(reply);
        }
    }
}
