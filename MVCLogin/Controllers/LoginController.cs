using MVCLogin.Models;
using MVCLogin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Authorize(Person personModel, LoginVM loginModel, InfoVM infoModel)
        {

            using (MVCLogin_dbEntities infodb = new MVCLogin_dbEntities())
            {
                personModel.UserName = loginModel.UserName;
                personModel.Password = loginModel.Password;
                int usernameLength = personModel.UserName.Length;
                string initial = personModel.UserName[0].ToString();
                string surname = personModel.UserName.Substring(1, usernameLength - 1);
                MVCLogin_dbEntities db = new MVCLogin_dbEntities(); // Person Table
                var personDetails = db.People.Where(x => x.Name.Substring(0, 1) == initial.ToString() && x.Surname == surname && x.Password == personModel.Password).FirstOrDefault();
                if (personDetails == null)
                {
                    loginModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", loginModel);
                } else
                {
                    var infoDetails = infodb.Infoes.Where(x => x.PersonId == personDetails.ID).FirstOrDefault();
                    Session["userID"] = personDetails.ID;
                    infoModel.Name = personDetails.Name;
                    infoModel.Surname = personDetails.Surname;
                    infoModel.Password = personDetails.Password;
                    infoModel.PersonId = personDetails.ID;
                    infoModel.TelNo = infoDetails.TelNo;
                    infoModel.CellNo = infoDetails.CellNo;
                    infoModel.AddressLine1 = infoDetails.AddressLine1;
                    infoModel.AddressLine2 = infoDetails.AddressLine2;
                    infoModel.AddressLine3 = infoDetails.AddressLine3;
                    infoModel.AddressCode = infoDetails.AddressCode;
                    infoModel.PostalAddress1 = infoDetails.PostalAddress1;
                    infoModel.PostalAddress2 = infoDetails.PostalAddress2;
                    infoModel.PostalCode = infoDetails.PostalCode;
                    // get prev login date
                    if (personDetails.LastLogin != null)
                    {
                        var prevLogin = personDetails.LastLogin;
                    }
                    // update with current login time
                    personDetails.LastLogin = DateTime.Now;
                    infodb.SaveChanges();
                    return RedirectToAction("GetInfo", "Info", infoModel);
                }
            }
        }
    }
}