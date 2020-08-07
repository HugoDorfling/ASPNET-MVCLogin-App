using System;
using System.Collections.Generic;
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
    }
}