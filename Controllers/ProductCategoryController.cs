using ShoppingCart.BusinessLayer.Services;
using ShoppingCart.DAL;
using ShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        IProductCategoryService _pcs;
        public ProductCategoryController()
        {
            _pcs = new ProductCategoryService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProductCategory()
        {
            if (Session["AdminName"] != null)
            {
                return View();
            }
            else
            { return new RedirectResult("~/Person/Login"); }//redirects getmenuitems if client is logged in
        }

        public JsonResult ProductCategorySel()
        {
            var result = _pcs.GetProductCategory();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CategoryForParentSel()
        {
            var result = _pcs.GetCategoryForParent();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult AddEditCategory(int CategoryId=0)
        {
            if (Session["AdminName"] != null)
            {
                ProductCategoryModel cm = new ProductCategoryModel();
                if (CategoryId > 0) { cm = _pcs.GetCategoryById(CategoryId); }
                return PartialView(cm);
            }
            else
            { return new RedirectResult("~/Person/Login"); }//redirects getmenuitems if client is logged in
        }

        [HttpPost]

        public ActionResult SaveCategory(ProductCategoryModel model)
        {
            _pcs.SaveCategory(model);
            return new RedirectResult("~/ProductCategory/ViewProductCategory");
        }

        public ActionResult DelCategory(int CategoryId = 0)
        {
            ProductCategoryModel model = new ProductCategoryModel();
            model.CategoryId = CategoryId;
            ReturnMessageModel m = _pcs.DelProductCategory(model);
            if(m.ReturnMessage != null)
            { ViewBag.message = "This category determines property of one of the products available, so it can not be deleted"; }
            return View("ViewProductCategory");
        }
    }
}