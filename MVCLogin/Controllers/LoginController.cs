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
        public ActionResult Authorize(Person personModel, LoginVM loginModel)
        {
            using (LoginDatabaseEntities db = new LoginDatabaseEntities())
            {
                personModel.UserName = loginModel.UserName;
                personModel.Password = loginModel.Password;
                int usernameLength = personModel.UserName.Length;
                string initial = personModel.UserName[0].ToString();
                string surname = personModel.UserName.Substring(1, usernameLength - 1);


                var userDetails = db.People.Where(x => x.Name.Substring(0,1) == initial.ToString() && x.Surname == surname && x.Password == personModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    loginModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", loginModel);
                } else
                {
                    Session["userID"] = userDetails.ID;
                    // get prev login date
                    if (userDetails.LastLogin != null)
                    {
                        var prevLogin = userDetails.LastLogin;
                    }
                    // update with current login time
                    userDetails.LastLogin = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Info");
                }
            }
        }
    }
}