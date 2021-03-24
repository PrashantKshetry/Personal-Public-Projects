using ShoppingCart.BusinessLayer.Services;
using ShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Routing;
using ShoppingCart.DAL;

namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductSubCategory
        IProductService _ps;

        public ProductController()
        {
            _ps = new ProductService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEditProduct(int ProductId=0)
        {
            if (Session["AdminName"] != null)
            {
                ProductModel pm = new ProductModel();
                if (ProductId > 0)
                {
                    pm = _ps.GetProductById(ProductId);
                }
                return PartialView(pm);
            }
            else
            { return new RedirectResult("~/Person/Login"); }
        }

        [HttpPost]

        public ActionResult SaveProduct(ProductModel model)
        {
            /*string filename = model.ImagePath;
            string filepath = "/Image/" + filename;
            model.ImagePath = filepath;*/
            string folderpath = "/Product/Image/";
            bool exists = Directory.Exists(Server.MapPath(folderpath));
            if (!exists) { Directory.CreateDirectory(Server.MapPath(folderpath)); }
            if (model.Image_Data != null)
            {
                model.ImagePath = folderpath + model.Image_Data.FileName;
                model.Image_Data.SaveAs(Server.MapPath("Image") + "/" + model.Image_Data.FileName);
            }
            _ps.SaveProduct(model);
            return new RedirectResult("~/Product/ViewProduct");
        }

        public ActionResult DelProduct(int ProductId)
        {
            ProductModel model = new ProductModel();
            model.ProductId = ProductId;
            ReturnMessageModel m = _ps.DelProduct(model);
            if (m.ReturnMessage != null)
            { ViewBag.message = "The product has already been ordered, so it can not be deleted"; }
            //return new RedirectResult("~/Product/ViewProduct");
            return View("ViewProduct");
        }

        public ActionResult ViewProduct()
        {
            if (Session["AdminName"] != null)
            {
                return View();
            }
            else
            { return new RedirectResult("~/Person/Login"); }//redirects ShowCart() if client is logged in
        }

        public JsonResult ProductSel()
        {
            var result = _ps.GetProduct();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMenuItems()
        {
            CategoryModel model = _ps.AllCategorySubCategorySel();
            return PartialView(model);
        }

        public ActionResult GetProductBySubCategoryId()
        {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                List<ProductModel> list = _ps.GetProductBySubCategoryId(id);
                return View(list);
        }

        public ActionResult DashBoard()
        {
            CategoryModel model = _ps.AllCategorySubCategorySel();
            return View(model);
        }
        public ActionResult ProductDetails(string Id, string Name, string Rate, string ImagePath)
        {
            ProductModel m = new ProductModel();
            m.ProductId = Convert.ToInt32(Id);
            m.ProductName = Name;
            m.Rate = Convert.ToInt32(Rate);
            m.ImagePath = ImagePath;
            return View(m);
        }
    }
}