using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private static IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("GetAll")]
        public GetInventoryResponse GetAll()
        {

            return _inventoryService.GetAll().GetAwaiter().GetResult();
        }

        [HttpPost("Buy")]
        public BuyResponse Buy(Dtos.BuyRequest buyRequest)
        {

            return _inventoryService.Buy(buyRequest).GetAwaiter().GetResult();
        }

    }
}
