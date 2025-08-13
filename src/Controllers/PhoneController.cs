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
        public ActionResult Createe(string F, string L, string A, string C, string S, string Z, string P)
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

        [Authorize]
        [HttpGet]
        public ActionResult AddNumber(int ID, string F)
        {
            ViewBag.PassedID = ID;
            ViewBag.Name = F;

            return View("AddNumberView");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddNumber(string P, int ID, string Name) 
        {
            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);

            sql.AddPhoneNumber(ID, P);


            var nextView = View(NumbersView(ID, Name));

            return RedirectToAction("NumbersView", new {ID = ID, firstName = Name });
        }

        public ActionResult DeleteContact(int ID)
        {

            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);

            sql.DeleteContact(ID);
            System.Diagnostics.Debug.WriteLine($"Contact with ID: {ID}, Deleted");
            return RedirectToAction("PhonebookView");
        }

        public ActionResult DeleteNumber(string num, string F, string ID) 
        {
            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);

            sql.DeleteNumber(num);
            return RedirectToAction("NumbersView", new { ID = ID, firstName = F });
        }


        [Authorize]
        [HttpGet]
        public ActionResult EditContact(int ID) 
        {
            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);

            Contact currentContact = sql.GetContact(ID);

            ViewBag.First = currentContact.firstName;
            ViewBag.Last = currentContact.lastName;
            ViewBag.Address = currentContact.address;
            ViewBag.City = currentContact.city;
            ViewBag.State = currentContact.State;
            ViewBag.Zip = currentContact.zip;
            ViewBag.ID = ID;

            return View("Edit");
        }
        
        [HttpPost]
        public ActionResult EditContactt(string F, string L, string A, string C, string S, int Z, int ID) 
        {
            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);

            Contact updatedContact = new Contact()
            {
                firstName = F,
                lastName = L,
                address = A,
                city = C,
                State = S,
                zip = Z
            };
            

            sql.EditContact(updatedContact, ID);


            return RedirectToAction("PhonebookView");
        }



        //add alternate details view to view actual numbers
        //Make alternate search view for phonebook 
        //make a post for this so itll return another view with search table
        [Authorize]
        public ActionResult PhonebookView(string search)
        {

            ViewBag.Userr = User.Identity.Name;

            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF","FNETINT01","fnetuser", "Fnetdev@2016", User.Identity.Name);

            IEnumerable<Contact> model = sql.GetContacts();

            //temp return value
            return View("PhonebookView", model);
        }

        [Authorize]
        public ActionResult NumbersView(int ID, string firstName) 
        {
            ViewBag.ID = ID;
            ViewBag.First = firstName;

            SqlModel model = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);
            
            return View(model);
        }

        public ActionResult _partialGrid(string search) 
        {
            ViewBag.Userr = User.Identity.Name;

            SqlModel sql = new SqlModel("D2E2SQLDEV16\\SQL19DEVF", "FNETINT01", "fnetuser", "Fnetdev@2016", User.Identity.Name);

            IEnumerable<Contact> model = sql.GetContacts();

            if (!String.IsNullOrWhiteSpace(search))
            {
                model = model.Where(s => s.firstName.Contains(search)).ToList();
            }

            return PartialView(model);
        }
    }
}