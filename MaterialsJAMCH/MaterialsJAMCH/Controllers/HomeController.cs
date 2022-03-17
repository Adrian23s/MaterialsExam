using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MaterialsJAMCH.Models;

namespace MaterialsJAMCH.Controllers
{
    public class HomeController : Controller
    {
        //Call we database
        Database.db dbMaterials = new Database.db();

        //Return view to create a Building
        public ActionResult CreateBuilding()
        {
            return View();
        }
        //Insert Building to db
        [HttpPost]
        public ActionResult CreateBuilding(FormCollection form)
        {
            Buildings bld = new Buildings();
            bld.Building = form["Building"];
            dbMaterials.CreateBuilding(bld);
            TempData["msg"] = "Record Created";
            return View();
        }

        //Create list to combobox for Buildings
        List<SelectListItem> listBLD;

        private void AddListBLD()
        {
            using(var db=new MaterialsEntitiesN()) //Add db from entities
            {
                listBLD = (from bld in db.Buildings
                           select new SelectListItem
                           {
                               Text = bld.Building,
                               Value = bld.PKBuilding.ToString()
                           }).ToList();
                listBLD.Insert(0, new SelectListItem { Text = "--Select Item--", Value = "" });
            }

        }

        //Return view to create a Customer
        public ActionResult CreateCustomer()
        {
            AddListBLD();
            ViewBag.List = listBLD;
            return View();
        }
        //Insert Customer to db
        [HttpPost]
        public ActionResult CreateCustomer(FormCollection form)
        {
            Customers cus = new Customers();

            cus.Customer = form["Customer"];
            cus.Prefix = form["Prefix"];
            cus.FKBuilding = int.Parse(form["PKBuilding"]);
            cus.Available = true;
            dbMaterials.CreateCustomer(cus);
            TempData["msg"] = "Record Created";
            AddListBLD();
            ViewBag.List = listBLD;
            return View();
        }

        //Create list to combobox for Customers
        List<SelectListItem> listCUS;

        private void AddListCUS()
        {
            using (var db = new MaterialsEntitiesN()) //Add db from entities
            {
                listCUS = (from cus in db.Customers
                           where cus.Available == true
                           select new SelectListItem
                           {
                               Text = cus.Customer,
                               Value = cus.PKCustomer.ToString()
                           }).ToList();
                listCUS.Insert(0, new SelectListItem { Text = "--Select Item--", Value = "" });
            }

        }

        //Return view to Create PartNumber
        public ActionResult CreatePartNumber()
        {
            AddListCUS();
            ViewBag.List = listCUS;
            return View();
        }

        //Insert Create PartNumber to db
        [HttpPost]
        public ActionResult CreatePartNumber(FormCollection form)
        {
            PartNumbers pn = new PartNumbers();

            pn.PartNumber = form["PartNumber"];
            pn.FKCustomer = int.Parse(form["PKCustomer"]);
            pn.Available = true;
            dbMaterials.CreatePartNumber(pn);
            TempData["msg"] = "Record Created";
            AddListCUS();
            ViewBag.List = listCUS;
            return View();
        }

        //Here call we table to show records of PartNumbers
        public ActionResult GetPartNumbers(string PartNumber, string Customer) {

            DataSet ds = dbMaterials.GetPartNumbers(PartNumber, Customer);
            ViewBag.lst = ds.Tables[0];

            return View();
        }


    }
}