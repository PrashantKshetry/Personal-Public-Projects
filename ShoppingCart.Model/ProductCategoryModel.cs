using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Model
{
    public class ProductCategoryModel
    {
        public int CategoryId { set; get; }

        public string CategoryName { set; get; }

        public int ParentCategoryId { set; get; }

        public string Parent_Category { set; get; }

        public string Category { set; get; }
    }
}
