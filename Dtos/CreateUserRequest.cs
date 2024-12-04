using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Dtos;

namespace Dtos
{
    public class CreateUserRequest
    {
        public string username { get; set; }
        public int cash { get; set; }
    }
}
