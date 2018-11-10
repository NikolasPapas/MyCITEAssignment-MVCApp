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
 * Attribute CONTROLER
 * Hear you can cortole that views: 
 * CiteApp_v1.2/CiteApp_v1.2/View/Attributes/ Index.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/Attributes/ Create.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/Attributes/ Delete.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/Attributes/ Details.cshtml
 * CiteApp_v1.2/CiteApp_v1.2/View/Attributes/ Edite.cshtml 
 */
namespace CiteApp_v1._2.Controllers
{
    public class AttributesController : Controller
    {
        private DBEntities db = new DBEntities();

        // GET: Attributes
        public ActionResult Index()
        {
            return View(db.Attributes.ToList());
        }

        // GET: Attributes/Details/id=?
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // GET: Attributes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attributes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ATTR_ID,ATTR_Name,ATTR_Value")] Models.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                attribute.ATTR_ID = Guid.NewGuid();
                db.Attributes.Add(attribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attribute);
        }

        // GET: Attributes/Edit/id=?
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // POST: Attributes/Edit/id=?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ATTR_ID,ATTR_Name,ATTR_Value")] Models.Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attribute);
        }

        // GET: Attributes/Delete/id=?
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // POST: Attributes/Delete/id=?
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Models.Attribute attribute = db.Attributes.Find(id);
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
