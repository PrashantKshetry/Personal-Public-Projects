using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingCart.Model
{

    public class ProductModel
    {
        public int ProductId { set; get; }

        public string ProductName { set; get; }

        public int Quantity { set; get; }

        public int Rate { set; get; }

        public int CategoryId { set; get; }

        public string ImagePath { set; get; }

        public HttpPostedFileBase Image_Data { set; get; } 

    }
}
