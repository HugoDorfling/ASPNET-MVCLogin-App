using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLogin.Models;
using MVCLogin.ViewModels;

namespace MVCLogin.Controllers
{
    public class InfoController : Controller
    {
        public static InfoVM currentInfoVM = new InfoVM();

        public ActionResult Index()
        {
            return View(currentInfoVM);
        }
        // GET: Info
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetInfo(InfoVM infoModel)
        {
            MVCLogin_dbEntities infodb = new MVCLogin_dbEntities();

            // query for getting data from database from joining two tables and storing data in infolist
            var infolist = (from Pers in infodb.People
                            join Inf in infodb.Infoes on Pers.ID equals Inf.PersonId
                            select new
                            { Pers.Name, Pers.Surname, Pers.Password, Inf.TelNo, Inf.CellNo, Inf.AddressLine1, Inf.AddressLine2, Inf.AddressLine3, Inf.AddressCode, Inf.PostalAddress1, Inf.PostalAddress2, Inf.PostalCode, Inf.PersonId });

            //Using foreach loop to fill data from infolist to List<InfoVM>
            foreach (var item in infolist)
            {
                if (item.Name == infoModel.Name && item.Surname == infoModel.Surname && infoModel.Password == item.Password)
                {
                    currentInfoVM.Name = item.Name;
                    currentInfoVM.Surname = item.Surname;
                    currentInfoVM.TelNo = item.TelNo;
                    currentInfoVM.CellNo = item.CellNo;
                    currentInfoVM.AddressLine1 = item.AddressLine1;
                    currentInfoVM.AddressLine2 = item.AddressLine2;
                    currentInfoVM.AddressLine3 = item.AddressLine3;
                    currentInfoVM.AddressCode = item.AddressCode;
                    currentInfoVM.PostalAddress1 = item.PostalAddress1;
                    currentInfoVM.PostalAddress2 = item.PostalAddress2;
                    currentInfoVM.PostalCode = item.PostalCode;
                    currentInfoVM.PersonId = item.PersonId;
                }

            }
            // Send List of InfoVM (ViewModel) to view
            if (infoModel.Name != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult GetInfoPartial()
        {
            if (currentInfoVM.Name != null)
            {
                return PartialView("_CreateOrUpdateInfoPartial", currentInfoVM);
            }
            return PartialView("");
        }

        public ActionResult CreateOrUpdateInfo(InfoVM info)
        {
            MVCLogin_dbEntities infodb = new MVCLogin_dbEntities();
            var personDetails = infodb.People.Where(b => b.ID == info.PersonId).SingleOrDefault();
            var infoDetails = infodb.Infoes.Where(b => b.PersonId == personDetails.ID).SingleOrDefault();

            if (personDetails != null && infoDetails != null)
            {
                string oldpass = "";
                oldpass = personDetails.Password;
                infoDetails.TelNo = info.TelNo;
                currentInfoVM.TelNo = info.TelNo;
                infoDetails.CellNo = info.CellNo;
                currentInfoVM.CellNo = info.CellNo;
                infoDetails.AddressLine1 = info.AddressLine1;
                currentInfoVM.AddressLine1 = info.AddressLine1;
                infoDetails.AddressLine2 = info.AddressLine2;
                currentInfoVM.AddressLine2 = info.AddressLine2;
                infoDetails.AddressLine3 = info.AddressLine3;
                currentInfoVM.AddressLine3 = info.AddressLine3;
                infoDetails.AddressCode = info.AddressCode;
                currentInfoVM.AddressCode = info.AddressCode;
                infoDetails.PostalAddress1 = info.PostalAddress1;
                currentInfoVM.PostalAddress1 = info.PostalAddress1;
                infoDetails.PostalAddress2 = info.PostalAddress2;
                currentInfoVM.PostalAddress2 = info.PostalAddress2;
                infoDetails.PostalCode = info.PostalCode;
                currentInfoVM.PostalCode = info.PostalCode;

                if (info.Password != null)
                {
                    personDetails.Password = info.Password;
                }
                else
                {
                    personDetails.Password = oldpass;
                }

                if (ModelState.IsValid)
                {
                    infodb.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            
            return Json(false, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult RefreshInfoPartial()
        {
            return PartialView("_InfoDetailsPartial", currentInfoVM);
        }

        public ActionResult Logout()
        {
            currentInfoVM.Name = "";
            currentInfoVM.Surname = "";
            currentInfoVM.TelNo = "";
            currentInfoVM.CellNo = "";
            currentInfoVM.AddressLine1 = "";
            currentInfoVM.AddressLine2 = "";
            currentInfoVM.AddressLine3 = "";
            currentInfoVM.AddressCode = "";
            currentInfoVM.PostalAddress1 = "";
            currentInfoVM.PostalAddress2 = "";
            currentInfoVM.PostalCode = "";
            currentInfoVM.PersonId = 3;
            currentInfoVM.Password = "";
            currentInfoVM.ConfirmPassword = "";
            return RedirectToAction("Index", "Login");
        }
    }
}