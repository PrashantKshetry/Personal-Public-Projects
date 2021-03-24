using ShoppingCart.BusinessLayer.Services;
using ShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class ProductSubCategoryController : Controller
    {
        // GET: ProductSubCategory
        IProductSubCategoryService _pscs;
        //IProductCategoryService _pcs;

        public ProductSubCategoryController()
        {
            _pscs = new ProductSubCategoryService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEditSubCategory(int SubCategoryId=0)
        {
            if (Session["AdminName"] != null)
            {
                ProductSubCategoryModel psm = new ProductSubCategoryModel();
                if (SubCategoryId > 0)
                {
                    psm = _pscs.GetSubCategoryById(SubCategoryId);
                }
                return PartialView(psm);
            }
            else
            { return new RedirectResult("~/Person/Login"); }//redirects getmenuitems if client is logged in
        }

        [HttpPost]

        public ActionResult SaveSubCategory(ProductSubCategoryModel model)
        {
            _pscs.SaveSubCategory(model);
            return new RedirectResult("~/ProductSubCategory/ViewProductSubCategory");
        }

        public ActionResult DelSubCategory(int SubCategoryId = 0)
        {
            ProductSubCategoryModel model = new ProductSubCategoryModel();
            model.SubCategoryId = SubCategoryId;
            _pscs.DelProductSubCategory(model);
            return View("ViewProductSubCategory");
        }

        public ActionResult ViewProductSubCategory()
        {
            if (Session["AdminName"] != null)
            {
                return View();
            }
            else
            { return new RedirectResult("~/Person/Login"); }//redirects getmenuitems if client is logged in
        }
        public JsonResult SubCategorySel()
        {
            var result = _pscs.GetProductSubCategory();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /*public JsonResult CategoryIdSel()
        {
            var result = _pscs.GetCategoryIdName();
            return Json(result, JsonRequestBehavior.AllowGet);
        }*/

    }
}