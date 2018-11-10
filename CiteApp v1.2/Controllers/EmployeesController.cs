using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CiteApp_v1._2.Models;

/**
 * EMPLOYEE CONTROLER
 * Hear you can cortole that views: 
 * CiteApp_v1.2/CiteApp_v1.2/View/Employees/ Index.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/Employees/ Create.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/Employees/ Delete.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/Employees/ Details.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/Employees/ Edite.cshtml 
 */

namespace CiteApp_v1._2.Controllers
{
    public class EmployeesController : Controller
    {
        private DBEntities db = new DBEntities();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Employee2);
            return View(employees.ToList());
        }

        // GET: Employees/Details/ID=?
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.EMP_Supervisor = new SelectList(db.Employees, "EMP_ID", "EMP_Name");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EMP_ID,EMP_Name,EMP_DateOfHire,EMP_Supervisor")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.EMP_ID = Guid.NewGuid();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMP_Supervisor = new SelectList(db.Employees, "EMP_ID", "EMP_Name", employee.EMP_Supervisor);
            return View(employee);
        }

        // GET: Employees/Edit/ID=?
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMP_Supervisor = new SelectList(db.Employees, "EMP_ID", "EMP_Name", employee.EMP_Supervisor);
            return View(employee);
        }

        // POST: Employees/Edit/ID=?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMP_ID,EMP_Name,EMP_DateOfHire,EMP_Supervisor")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMP_Supervisor = new SelectList(db.Employees, "EMP_ID", "EMP_Name", employee.EMP_Supervisor);
            return View(employee);
        }

        // GET: Employees/Delete/ID=?
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/ID=?
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
