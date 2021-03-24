using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DAL
{
    public class ReturnMessageModel
    {
        public int ReturnInteger { get; set; }
        public string ReturnMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
