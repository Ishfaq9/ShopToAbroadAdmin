using ShopToAbroad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopToAbroad.Controllers
{
    public class RegisterController : Controller
    {
        ShopToAbroadEntities db = new ShopToAbroadEntities();

        // GET: Account
        public ActionResult Index()
        {
            this.GetDefaultData();

            var usr = db.Customers.Find(TempShpData.UserID);
            return View(usr);

        }


        //REGISTER CUSTOMER
        [HttpPost]

        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(cust);
                db.SaveChanges();

                Session["username"] = cust.UserName;
                TempShpData.UserID = GetUser(cust.UserName).CustomerID;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public Customer GetUser(string _usrName)
        {
            var cust = (from c in db.Customers
                        where c.UserName == _usrName
                        select c).FirstOrDefault();
            return cust;
        }
        public ActionResult Register()
        {
            return View();
        }

    }
}