using Dtos;
using Greet;
using Grpc.Net.Client;
using InventoryMicroservice;
using static Greet.Greeter;
using static InventoryMicroservice.inventoryproto;
namespace WebAPI.Services
{
    public class InventoryService: IInventoryService
    {
        private static inventoryprotoClient _inventoryprotoClient;
        public InventoryService(GreeterClient greeterClient, inventoryprotoClient inventoryprotoClient)
        {
            _inventoryprotoClient = inventoryprotoClient;
        }

        public async Task<GetInventoryResponse> GetAll()
        {
            GetInventoryResponse response = new GetInventoryResponse();

            GetInventoryGrpcRequest request = new GetInventoryGrpcRequest();

            GetInventoryGrpcResponse grpcResponse = await _inventoryprotoClient.GetAllAsync(request);

            foreach (inventoryItem inventoryItem in grpcResponse.Inventory)
            {
                Inventory inventory = new Inventory();
                inventory.id = inventoryItem.Id;
                inventory.item = inventoryItem.Item;
                inventory.item_count = inventoryItem.ItemCount;
                inventory.price = inventoryItem.Price;
                response.inventory.Add(inventory);

            }

            return response;
        }
        public async Task<BuyResponse> Buy(Dtos.BuyRequest buyRequest)
        {
            InventoryMicroservice.BuyRequest request = new InventoryMicroservice.BuyRequest();
            BuyReply grpcResponse = await _inventoryprotoClient.BuyAsync(request);

            BuyResponse response = new BuyResponse();
            response.statusCode.code = grpcResponse.Code;
            response.statusCode.message = grpcResponse.Message;

            return response;
        }
    }
}
