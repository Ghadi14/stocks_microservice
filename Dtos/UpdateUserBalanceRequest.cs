using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class UpdateUserBalanceRequest
    {
        public string username { get; set; }
        public int cash { get; set; }
    }
}
