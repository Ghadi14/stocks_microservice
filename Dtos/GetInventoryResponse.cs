using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Dtos;

namespace Dtos
{
    public class GetInventoryResponse: GlobalResponse
    {
        public List<Inventory> inventory { get; set; } = new List<Inventory>();

    }
    public class Inventory
    {
        public int id { get; set; }
        public string item { get; set; }
        public int item_count { get; set; }
        public int price { get; set; }
    }
}
