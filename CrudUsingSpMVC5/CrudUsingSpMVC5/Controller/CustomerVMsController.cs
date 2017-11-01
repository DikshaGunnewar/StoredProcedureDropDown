using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrudUsingSpMVC5.Models;
using System.Diagnostics;

namespace CrudUsingSpMVC5.Controllers
{
    public class CustomerVMsController : Controller
    {
        private CrudUsingSpMVC5Context db = new CrudUsingSpMVC5Context();

        #region--LogMethod--
        /// <summary>
        /// To check wheather MVC is 
        /// using Stored procedure
        /// or not
        /// </summary>
        public CustomerVMsController()
        {
            db.Database.Log = l => Debug.Write(l);
        }

        #endregion

        #region--Get Index  --
        // GET: CustomerVMs
        public ActionResult Index()
        {
            return View(db.CustomerVMs.ToList());
        }
        #endregion

        #region--Get Method for Get All Details --
        // GET: CustomerVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerVM customerVM = db.CustomerVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            return View(customerVM);
        }

        #endregion

        #region--Get Method for Create --
        // GET: CustomerVMs/Create
        public ActionResult Create()
        {
            ViewBag.StateList = db.States;
            var model = new CustomerVM();
            return View(model);
        }

        #endregion

        #region--Post Method for Create --
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,CurrentAddress,PermanantAddress,State,City")] CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                db.CustomerVMs.Add(customerVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateList = db.States;
            return View(customerVM);
        }

        #endregion

        #region--Get Method for Edit --
        // GET: CustomerVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            CustomerVM customerVM = db.CustomerVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateList = db.States;
            return View(customerVM);
        }

        #endregion

        #region--Post Method for Edit --
        /// <summary>
        /// Edit method is working
        /// but when we click edit link
        /// it will show selected state but not showing selected city
        /// when we change state then after that it is showing state and city list.
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
        // POST: CustomerVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,PermanantAddress,CurrentAddress,State,City")] CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateList = db.States;
            return View(customerVM);
        }

        #endregion

        #region--Get Method for Delete --
        // GET: CustomerVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerVM customerVM = db.CustomerVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            return View(customerVM);
        }

        #endregion

        #region--Post Method for Delete --
        // POST: CustomerVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerVM customerVM = db.CustomerVMs.Find(id);
            db.CustomerVMs.Remove(customerVM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region--Fill City dropDown Method--
        /// <summary>
        /// Fill city Method For DrowDown
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult FillCity(int state)
        {
            var cities = db.Cities.Where(c => c.StateId == state);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region--Despose Method--
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }

}
