using Dapper;
using Dtos;
using Newtonsoft.Json;
using PostgresqlHelper;
using System.Data;

namespace InventoryMicroservice.RepositoryService
{
    public class InventoryRepository: IInventoryRepository
    {
        private static IPostgresService _postgresService;

        public InventoryRepository(IPostgresService postgresService)
        {
            _postgresService = postgresService;
        }

        public GetInventoryResponse GetInventory()
        {
            GetInventoryResponse response = new GetInventoryResponse();

            DynamicParameters parameters = new DynamicParameters();


            parameters = _postgresService.ExecuteStoredProcedure("public.api_inventory", parameters).Result;

            int statuscode = parameters.Get<int>("status_code");

            string jsonResponseString = parameters.Get<string>("p_inventory");


            response.inventory = JsonConvert.DeserializeObject<List<Inventory>>(jsonResponseString);

            return response;
        }

        public BuyResponse Buy(Dtos.BuyRequest request)
        {
            BuyResponse response = new BuyResponse();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("p_username", request.username, DbType.String);
            parameters.Add("quantity", request.quantity, DbType.Int32);
            parameters.Add("item_id", request.itemId, DbType.Int32);

            parameters = _postgresService.ExecuteStoredProcedure("public.buy_item", parameters).Result;

            int statuscode = parameters.Get<int>("status_code");
            string message = parameters.Get<string>("message");

            response.statusCode.code = statuscode;
            response.statusCode.message = message;

            return response;
        }
    }
}
