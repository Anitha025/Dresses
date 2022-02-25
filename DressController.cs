using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableData.Models;

namespace TableData.Controllers
{
    public class DressController : Controller
    {
        // GET: Dress
       public ActionResult PlaceOrder()
        {
            List<Objects> ord = new List<Objects>
            {
                new Objects { DressBrand ="Biba",DressID=101,DressType="Saree",DressPrice=4000},
                new Objects { DressBrand ="H&M",DressID=102,DressType="Jeans",DressPrice=2500},
                new Objects { DressBrand ="ZARA",DressID=103,DressType="Party Dress",DressPrice=3000}
            };
            
            return View(ord);
        }
        public ActionResult GetDresses()
        {
            var con = new DBcon();
            var data = con.GetDresses();
            return View(data);
        }
        public ActionResult FindDress(string id)
        {
            int DID = Convert.ToInt32(id);
            var con = new DBcon();
            try
            {
                var dress = con.FindDress(DID);
                return View(dress);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult AddDress()
        {
            var com = new DBcon();
            return View(new Objects());
        }
        [HttpPost]
        public ActionResult AddDress(Objects ad)
        {
            var com = new DBcon();
            try
            {
                com.AddDress(ad);
                return View(new Objects());
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ViewBag.ErrorMessage = message;
                return View(new Objects());
            }

        }
        public ActionResult UpdateDress(string id)
        {
            int dressId = Convert.ToInt32(id);
            var con = new DBcon();
            try
            {
                var dress = con.FindDress(dressId);
                return View(dress);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult UpdateDress(Objects drs)
        {
            
            var con = new DBcon();
            try
            {
                con.UpdateDress(drs);
                return RedirectToAction("GetDresses");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult DeleteDress(string id)
        {
            var con = new DBcon();
            int dressId = Convert.ToInt32(id);
            try
            {
                con.DeleteDress(dressId);
                return RedirectToAction("GetDresses");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }


    }
}