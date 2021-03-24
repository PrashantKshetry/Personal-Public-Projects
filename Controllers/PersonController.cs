using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.BusinessLayer.Services;
using ShoppingCart.Model;
using ShoppingCart.DAL;

namespace ShoppingCart.Areas.Person.Controllers
{
    public class PersonController : Controller
    {
        // private IPersonService ps;
        IPersonService _ps;
        public PersonController()
        {
            _ps =new PersonService();
        }
       // PrashantEntities db = new PrashantEntities();
        // GET: PersonArea/Person
        public ActionResult Index()
        {
             
            return View();
        }

        public ActionResult Login()
        {
            if (Session["AdminName"] == null && Session["LogName"] == null) { return View(); }
            else if (Session["LogName"] != null)
            { return new RedirectResult("~/Customer/ShowCart"); }
            else
            { return new RedirectResult("~/Person/ViewPerson"); }
        }
        [HttpPost]
        public ActionResult Validation(LoginModel lm)
        {
            //if (LogName != "" && LogPasswd != "")
            //{ return new RedirectResult("~/Product/GetMenuItems"); }
             return View(lm);
            
        }

        public ActionResult CartBoard(string LogName="", string AdminName="")
        {
            if(LogName=="" && AdminName=="")
            {
                Session["LogName"] = null;
                Session["AdminName"] = null;
                return new RedirectResult("~/Person/Login");
            }
            else if(LogName!="")
            {
                Session["LogName"] = LogName;
                Session["AdminName"] = null;
                if (Session["Name"] == null)
                {
                    return new RedirectResult("~/Customer/ShowCart");
                }
                else
                {
                    return new RedirectResult("~/Customer/Order");
                }
            }
            else
            {
                Session["LogName"] = null;
                Session["AdminName"] = AdminName;
                return new RedirectResult("~/Person/ViewPerson");
            }
            
        }
        public ActionResult AddEditPerson(int PersonId = 0)
        {
            PersonModel pm = new PersonModel();
            if (PersonId > 0)
            {
                pm = _ps.PersonByIdSel(PersonId);
            }
            return PartialView(pm);
        }
        [HttpPost]
        public ActionResult SavePerson(PersonModel model)
        {
            _ps.SavePerson(model);
            return new RedirectResult("~/Person/ViewPerson");
        }
        
        public ActionResult DeletePerson(int PersonId=0)
        {
            PersonModel model = new PersonModel();
            model.PersonId = PersonId;
            _ps.DeletePerson(model);
            return new RedirectResult("~/Person/ViewPerson");
        }

        public ActionResult ViewPerson()
        {
            if (Session["AdminName"] != null)
            {
                return View();
            }
            else
            { return new RedirectResult("~/Person/Login"); }//redirects getmenuitems if client is logged in
        }
        
        public JsonResult PersonSel()
        {
          var result= _ps.PersonSel();
          return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoginDataSel()
        {
            var result = _ps.GetLoginData();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAdmin()
        {
            var result = _ps.GetAdminData();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PersonLogout()
        {
            Session["LogName"] = null;
            Session["AdminName"] = null;
            return new RedirectResult("~/Person/Login");
        }

        public ActionResult ViewProfile()
        {
            if(Session["LogName"] != null)
            {
                string username = Session["LogName"].ToString();
                PersonModel m = _ps.PersonByUserSel(username);
                return View(m);
            }
            else { return new RedirectResult("~/Person/Login"); }
        }  
    }
}