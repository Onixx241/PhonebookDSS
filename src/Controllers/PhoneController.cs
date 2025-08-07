using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhonebookwithAuth.Controllers
{
    public class PhoneController : Controller
    {


        [Authorize]
        [HttpGet]
        public ActionResult Create() 
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Createe(string F, string L, string A, string C, string S, string Z) 
        {
            //why is this not running
            Contact contact = new Contact()
            {
                firstName = F,
                lastName = L,
                address = A,
                city = C,
                State = S,
                zip = Convert.ToInt32(Z)
            };

            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);

            sql.CreateContact(contact, User.Identity.Name);
            System.Diagnostics.Debug.WriteLine(" This is working!");
            return RedirectToAction("PhonebookView");

        }


        [Authorize]
        public ActionResult PhonebookView()
        {

            ViewBag.Userr = User.Identity.Name;
            ViewBag.bag = User.Identity.AuthenticationType;

            SqlModel model = new SqlModel("D2E2SQLDEV16\\SQL19DEVF","FNETINT01","fnetuser", "Fnetdev@2016", User.Identity.Name);

            
            return View("PhonebookView", model);
        }


        public ActionResult CreateContact(string F, string L, string A, string C, string S, string Z) 
        {
            Contact contact = new Contact()
            {
                firstName = F,
                lastName = L,
                address = A,
                city = C,
                State = S,
                zip = Convert.ToInt32(Z)
            };

            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);

            sql.CreateContact(contact, User.Identity.Name);
            System.Diagnostics.Debug.WriteLine(" This is working!");
            return RedirectToAction("PhonebookView");

        }
    }
}
