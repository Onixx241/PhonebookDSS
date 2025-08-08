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

        [Authorize]
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

            SqlModel sql = new SqlModel("", User.Identity.Name);

            sql.CreateContact(contact, User.Identity.Name);
            System.Diagnostics.Debug.WriteLine(" This is working!");
            return RedirectToAction("PhonebookView");

        }

        //continue here
        public ActionResult DeleteContact(int ID)
        {

            SqlModel sql = new SqlModel("", User.Identity.Name);

            sql.DeleteContact(ID);
            System.Diagnostics.Debug.WriteLine($"Contact with ID: {ID}, Deleted");
            return RedirectToAction("PhonebookView");
        }



        //add alternate details view to view actual numbers
        [Authorize]
        public ActionResult PhonebookView()
        {

            ViewBag.Userr = User.Identity.Name;

            SqlModel model = new SqlModel("", User.Identity.Name);

            //temp return value
            return View("PhonebookView", model);
        }
        
    }
}
