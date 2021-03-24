using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Model
{
    public class CategoryModel
    {
      public CategoryModel()
        {
            ParentCategory = new List<ProductCategoryModel>();
            Category = new List<ProductCategoryModel>();
            Product = new List<ProductModel>();
        }

        public List<ProductCategoryModel> ParentCategory { get; set; }
        public List<ProductCategoryModel> Category { get; set; }
        public List<ProductModel> Product { get; set; }
    }
}
