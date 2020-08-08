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
        // GET: Info
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            LoginDatabaseEntities1 infodb = new LoginDatabaseEntities1();
            List<InfoVM> InfoVMList = new List<InfoVM>(); // to hold list of person and info details
            // query for getting data from database from joining two tables and storing data in infolist
            var infolist = (from Pers in infodb.People join Inf in infodb.Infoes on Pers.ID equals Inf.PersonId select new 
            { Pers.Name, Pers.Surname, Inf.TelNo, Inf.CellNo, Inf.AddressLine1, Inf.AddressLine2, Inf.AddressLine3, Inf.AddressCode, Inf.PostalAddress1, Inf.PostalAddress2, Inf.PostalCode }).ToList();
            
            //Using foreach loop to fill data from infolist to List<InfoVM>
            foreach (var item in infolist)
            {
                InfoVM objivm = new InfoVM(); // ViewModel
                objivm.Name = item.Name;
                objivm.Surname = item.Surname;
                objivm.TelNo = item.TelNo;
                objivm.CellNo = item.CellNo;
                objivm.AddressLine1 = item.AddressLine1;
                objivm.AddressLine2 = item.AddressLine2;
                objivm.AddressLine3 = item.AddressLine3;
                objivm.AddressCode = item.AddressCode;
                objivm.PostalAddress1 = item.PostalAddress1;
                objivm.PostalAddress2 = item.PostalAddress2;
                objivm.PostalCode = item.PostalCode;
                InfoVMList.Add(objivm);
            }
            // Send List of InfoVM (ViewModel) to view 
            return View(InfoVMList);
        }

        public ActionResult GetInfoPartial()
        {
            LoginDatabaseEntities1 infodb = new LoginDatabaseEntities1();

            var info = (from Pers in infodb.People
                            join Inf in infodb.Infoes on Pers.ID equals Inf.PersonId
                            select new
                            { Pers.Name, Pers.Surname, Pers.Password, Inf.PersonId, Inf.TelNo, Inf.CellNo, Inf.AddressLine1, Inf.AddressLine2, Inf.AddressLine3, Inf.AddressCode, Inf.PostalAddress1, Inf.PostalAddress2, Inf.PostalCode });

            foreach (var item in info)
            {
                InfoVM objivm = new InfoVM(); // ViewModel
                objivm.PersonId = item.PersonId;
                objivm.Name = item.Name;
                objivm.Surname = item.Surname;
                objivm.TelNo = item.TelNo;
                objivm.CellNo = item.CellNo;
                objivm.AddressLine1 = item.AddressLine1;
                objivm.AddressLine2 = item.AddressLine2;
                objivm.AddressLine3 = item.AddressLine3;
                objivm.AddressCode = item.AddressCode;
                objivm.PostalAddress1 = item.PostalAddress1;
                objivm.PostalAddress2 = item.PostalAddress2;
                objivm.PostalCode = item.PostalCode;
                objivm.Password = item.Password;
                return PartialView("_CreateOrUpdateInfoPartial", objivm);
            }

            return PartialView("");
        }

        public ActionResult CreateOrUpdateInfo(InfoVM info)
        {
            LoginDatabaseEntities1 staticdb = new LoginDatabaseEntities1();

            var staticinfo = (from Pers in staticdb.People
                        join Inf in staticdb.Infoes on Pers.ID equals Inf.PersonId
                        select new
                        { Pers.Name, Pers.Surname});

            Person pers = new Person();
            foreach (var item in staticinfo)
            {
                pers.Name = item.Name;
                pers.Surname = item.Surname;
            }
            Info inf = new Info();
            inf.PersonId = info.PersonId;
            inf.TelNo = info.TelNo;
            inf.CellNo = info.CellNo;
            inf.AddressLine1 = info.AddressLine1;
            inf.AddressLine2 = info.AddressLine2;
            inf.AddressLine3 = info.AddressLine3;
            inf.AddressCode = info.AddressCode;
            inf.PostalAddress1 = info.PostalAddress1;
            inf.PostalAddress2 = info.PostalAddress2;
            inf.PostalCode = info.PostalCode;
            pers.ID = info.PersonId;
            pers.Password = info.Password;

            if (ModelState.IsValid)
            {
                LoginDatabaseEntities1 infodb = new LoginDatabaseEntities1();
                infodb.Infoes.AddOrUpdate(inf);
                infodb.People.AddOrUpdate(pers);
                infodb.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RefreshInfoPartial()
        {
            LoginDatabaseEntities1 infodb = new LoginDatabaseEntities1();
            List<InfoVM> InfoVMList = new List<InfoVM>(); // to hold list of person and info details
            // query for getting data from database from joining two tables and storing data in infolist
            var infolist = (from Pers in infodb.People
                            join Inf in infodb.Infoes on Pers.ID equals Inf.PersonId
                            select new
                            { Pers.Name, Pers.Surname, Inf.TelNo, Inf.CellNo, Inf.AddressLine1, Inf.AddressLine2, Inf.AddressLine3, Inf.AddressCode, Inf.PostalAddress1, Inf.PostalAddress2, Inf.PostalCode}).ToList();

            //Using foreach loop to fill data from infolist to List<InfoVM>
            foreach (var item in infolist)
            {
                InfoVM objivm = new InfoVM(); // ViewModel
                objivm.Name = item.Name;
                objivm.Surname = item.Surname;
                objivm.TelNo = item.TelNo;
                objivm.CellNo = item.CellNo;
                objivm.AddressLine1 = item.AddressLine1;
                objivm.AddressLine2 = item.AddressLine2;
                objivm.AddressLine3 = item.AddressLine3;
                objivm.AddressCode = item.AddressCode;
                objivm.PostalAddress1 = item.PostalAddress1;
                objivm.PostalAddress2 = item.PostalAddress2;
                objivm.PostalCode = item.PostalCode;
                InfoVMList.Add(objivm);
            }
            return PartialView("_InfoDetailsPartial", InfoVMList);
        }
    }
}