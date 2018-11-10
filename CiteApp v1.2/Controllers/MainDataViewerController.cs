using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CiteApp_v1._2.Models;
using System.Dynamic;
using System.Net;
using System.Data.Entity;


/**
 * MAIN CONTROLER
 * Created to managing the employees and their attributes
 * Hear you can cortole that views: 
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ Index.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ CreateEMP.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ CreateATTR.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ DeleteEMP.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ DeleteATTR.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ DetailsEMP.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ DetailsATTR.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ EditeEMP.cshtml 
 * CiteApp_v1.2/CiteApp_v1.2/View/MainDataViewer/ EditeATTR.cshtml
 */


namespace CiteApp_v1._2.Controllers
{
    public class MainDataViewerController : Controller
    {
        private DBEntities db = new DBEntities();

        /* 
         * GET: MainDataViewer
         * {HTTP}/MainDataViewer/Index
         * That methode use the Index.cshtml View
         * To display Employees and Attributes
         */
        public ActionResult Index()
        {
            var employees = db.Employees;
            var attributes = db.Attributes;

            AllDataViewer modelView = new AllDataViewer();

            modelView.listOfEmployee = employees.ToList();
            modelView.listOfAttribute = attributes.ToList();

            return View(modelView);
        }



        /* 
         * GET: MainDataViewer/DetailsEMP/ID=?
         * {HTTP}/MainDataViewer/DetailsEMP
         * That methode use the DetailsEMP.cshtml View
         * To display specific Employee
         */
        public ActionResult DetailsEMP(Guid? id)
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

       
        /* 
         * GET: MainDataViewer/CreateEMP
         * {HTTP}/MainDataViewer/CreateEMP
         * That methode use the CreateEMP.cshtml View
         * To Create View to fill values for a new Employee
         */
        public ActionResult CreateEMP()
        {
            ViewBag.EMP_Supervisor = new SelectList(db.Employees, "EMP_ID", "EMP_Name");
            return View();
        }


        /* 
         * POST: MainDataViewer/CreateEMP
         * {HTTP}/MainDataViewer/CreateEMP
         * That methode use the CreateEMP.cshtml View
         * To Create Employee and save it to DataBase
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEMP([Bind(Include = "EMP_ID,EMP_Name,EMP_DateOfHire,EMP_Supervisor")] Employee employee)
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


        
        /* 
         * GET: MainDataViewer/EditEMP/ID=?
         * {HTTP}/MainDataViewer/EditEMP
         * That methode use the EditEMP.cshtml View
         * To modify Values from specific Employee
         */
        public ActionResult EditEMP(Guid? id)
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

        
        /* 
        * POST: MainDataViewer/EditEMP/ID=?
        * {HTTP}/MainDataViewer/EditEMP
        * That methode use the EditEMP.cshtml View
        * To update Database whith the modify Values from specific Employee
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEMP([Bind(Include = "EMP_ID,EMP_Name,EMP_DateOfHire,EMP_Supervisor")] Employee employee)
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



        /* 
        * GET: MainDataViewer/DeleteEMP/ID=?
        * {HTTP}/MainDataViewer/DeleteEMP
        * That methode use the DeleteEMP.cshtml View
        * To Create View to fill values from chose Employee
        */
        public ActionResult DeleteEMP(Guid? id)
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


        /* 
        * POST: MainDataViewer/DeleteEMP/ID=?
        * {HTTP}/MainDataViewer/DeleteEMP.
        * That methode use the DeleteEMP.cshtml View
        * To Delete Employee and Delete from DataBase
        */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedEMP(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }











       
        /* 
        * GET: MainDataViewer/DetailsATTR/ID=?
        * {HTTP}/MainDataViewer/DetailsATTR
        * That methode use the DetailsATTR.cshtml View
        * To display specific Attribute and the Employ owner
        */
        public ActionResult DetailsATTR(Guid? id , Guid? idAttr)
        {


            if (id == null)
            {
                if (idAttr == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            Employee employee = db.Employees.Find(id);
            Models.Attribute attitube = db.Attributes.Find(idAttr);

            if (employee == null | attitube == null)
            {
                return HttpNotFound();
            
            }
               
            ViewBag.Attribute = attitube;
            return View(employee);
        }

       
        /* 
         * GET: MainDataViewer/CreateATTR/EMP_ID=?
         * {HTTP}/MainDataViewer/CreateATTR
         * That methode use the CreateATTR.cshtml View
         * To Create View to fill values for a new Attribute and display the owner Employee
         */
        public ActionResult CreateATTR()
        {
            ViewBag.EMP_List= db.Employees;
            return View();
        }

    
        /* 
         * POST: MainDataViewer/CreateATTR/EMP_ID=?
         * {HTTP}/MainDataViewer/CreateATTR
         * That methode use the CreateATTR.cshtml View
         * To Create Attribute and fill it with values from View and Save it to Database
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateATTR([Bind(Include = "ATTR_ID,ATTR_Name,ATTR_Value,ATTR_EMP_ID")] Models.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                attribute.ATTR_ID = Guid.NewGuid();
                
                //Add asoAssociated betoen Employee and Attributes
                
                db.Employees.Find(attribute.ATTR_EMP_ID).Attributes.Add(attribute);
                db.Attributes.Add(attribute);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            return View(attribute);
        }


        
      /* 
      * GET: MainDataViewer/EditATTR/ID=?
      * {HTTP}/MainDataViewer/EditATTR
      * That methode use the EditATTR.cshtml View
      * To modify Values from specific Attribute and display the owner Employee
      */
        public ActionResult EditATTR(Guid? id, Guid? idAttr)
        {
            if (id == null | idAttr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Employee employee = db.Employees.Find(id);
            Models.Attribute attribute = db.Attributes.Find(idAttr);
            if (employee == null | attribute == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee = employee;
            return View(attribute);
        }

       
       /* 
       * POST: MainDataViewer/EditATTR/ID=?
       * {HTTP}/MainDataViewer/EditATTR
       * That methode use the EditATTR.cshtml View
       * To update Database whith the modify Values from specific Attribute
       */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditATTR([Bind(Include = "ATTR_ID,ATTR_Name,ATTR_Value")] Models.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attribute);
        }


        
       /* 
       * GET: MainDataViewer/DeleteATTR/ID=?
       * {HTTP}/MainDataViewer/DeleteATTR.
       * That methode use the DeleteATTR.cshtml View
       * To Create View to fill values from chose Employee
       */
        public ActionResult DeleteATTR(Guid? id, Guid? idAttr)
        {
            if (id == null | idAttr == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            Models.Attribute attribute = db.Attributes.Find(idAttr);
            if (employee == null | attribute == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee = employee;
            return View(attribute);
        }

       /* 
       * POST: MainDataViewer/DeleteATTR/ID=?
       * {HTTP}/MainDataViewer/DeleteATTR
       * That methode use the DeleteATTR.cshtml View
       * To Delete Employee and Delete from DataBase
       */
        [HttpPost, ActionName("DeleteATTR")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedATTR(Guid id, Guid? idAttr)
        {
            Models.Employee employee = db.Employees.Find(id);
            Models.Attribute attribute = db.Attributes.Find(idAttr);
            employee.Attributes.Remove(attribute);
            db.Attributes.Remove(attribute);
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