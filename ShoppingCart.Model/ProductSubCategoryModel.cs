using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Model
{
    public class ProductSubCategoryModel
    {
        public int SubCategoryId { set; get; }

        public string SubCategoryName { set; get; }

        public int CategoryId { set; get; }
    }
}
