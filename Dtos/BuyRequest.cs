using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class BuyRequest
    {
        public int  itemId { get; set; }
        public string username { get; set; }
        public int quantity { get; set; }
    }
}
