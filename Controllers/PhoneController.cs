using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhonebookwithAuth.Controllers
{
    public class PhoneController : Controller
    {
        public ActionResult AddContact()
        {
            //this is going to call addcontact stored procedure
            return new EmptyResult();
        }
        public ActionResult DeleteContact()
        {
            //this is going to call deletecontact stored procedure
            return new EmptyResult();
        }
        [Authorize]
        public ActionResult PhonebookView()
        {

            ViewBag.Userr = User.Identity.Name;
            string[] arr = { "hello", "Hello1", "Hello1", "Hello1", "Hello1", "Hello1", "Hello1" };
            ViewBag.TestContacts = arr;
            ViewBag.Message = "Test";
            //temp return value
            return View("PhonebookView");
        }
    }
}