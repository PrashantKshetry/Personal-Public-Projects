using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Model;
using ShoppingCart.BusinessLayer.Services;

namespace ShoppingCart.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        ICustomerService _cs;
        public CustomerController()
        {
            _cs = new CustomerService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order(string Id,string Name, string Rate, string ImagePath)
        {
            if(Session["LogName"] != null && Session["AdminName"] == null)
            {
                if (Session["Name"] != null)
                {
                    string id = Session["Id"].ToString();
                    string name = Session["Name"].ToString();
                    string rate = Session["Rate"].ToString();
                    string imagepath = Session["ImagePath"].ToString();
                    Session["Id"] = null;
                    Session["Name"] = null;
                    Session["Rate"] = null;
                    Session["ImagePath"] = null;
                    return new RedirectResult("~/Customer/Order/"+id+"?Name="+name+"&Rate="+rate+"&ImagePath="+imagepath);
                }
                else
                {
                    CartModel m = new CartModel();
                    m.UserName = Session["LogName"].ToString();
                    m.ProductId = Convert.ToInt32(Id);
                    m.ProductName = Name;
                    m.Rate = Convert.ToInt32(Rate);
                    m.ImagePath = ImagePath;
                    return View(m);
                }   
            }
            else
            {
                Session["Id"] = Id;
                Session["Name"] = Name;
                Session["Rate"] = Rate;
                Session["ImagePath"] = ImagePath;
                return new RedirectResult("~/Person/Login");
            }
        }

        [HttpPost]
        public ActionResult SaveOrder(CartModel m)
        {
            _cs.CustomerInsert(m);
            _cs.PrdQtyUpd(m);
            return new RedirectResult("~/Customer/ShowCart");
        }

        public ActionResult ShowCart()
        {
            if (Session["LogName"] != null)
            {
                return View();
            }
            else
            {
                return new RedirectResult("~/Person/Login");
            }
        }

        public ActionResult ShowAllCart()
        {
            if (Session["AdminName"] != null)
            {
                return View();
            }
            else
            {
                return new RedirectResult("~/Person/Login");
            }
        }

        public JsonResult CartSel()
        {
            CartModel cm = new CartModel();
            if (Session["LogName"] != null)
            { cm.UserName = Session["LogName"].ToString(); }
            else { cm.UserName = ""; }
            var result = _cs.CartSel(cm);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /*public JsonResult GetUserData()
        {
            string UserName;
            var result = new PersonModel(); 
            if (Session["LogName"] != null)
            {
                UserName = Session["LogName"].ToString();
                result = _cs.GetPersonByUser(UserName);
            }
            else
            {
                result = null;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }*/
    }
}