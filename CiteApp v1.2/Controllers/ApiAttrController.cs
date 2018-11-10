using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CiteApp_v1._2.Models;
using System.Web.Script.Serialization;
using System.Web.Http.Results;
using System.Web.Mvc;

/**
 * WEB API CONTROLER
 * That controler used wen you call it 
 * and return Json Strings about the Attributes
 * you can call it from that linc:
 * {HTTP}/api/ApiAttr
 * {HTTP}/api/ApiAttr/ID=?
 * {HTTP}/api/ApiAttr/String EMP_Name,DateTime EMP_EMP_DateOfHire ,Guid idSuper 
 */

namespace CiteApp_v1._2.Controllers
{
    public class ApiAttrController : ApiController
    {
        private DBEntities db = new DBEntities();

        /**
         * GET: api/ApiAttr
         * {HTTP}/ api/ApiAttr
         * That methode return All Attributes
         */
        [System.Web.Http.HttpGet]
        public JsonResult Get()
        {
            var attributes = db.Attributes;
            String[,] table = new String[attributes.Count(), 3];
            int i = 0;
            foreach (Models.Attribute attr in attributes)
            {
                table[i, 0] = attr.ATTR_ID.ToString();
                table[i, 1] = attr.ATTR_Name.ToString();
                table[i, 2] = attr.ATTR_Value.ToString();
                i++;
            }
            JsonResult retData = new JsonResult();
            retData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            retData.MaxJsonLength = attributes.Count();
            retData.Data = table;
            return retData;
        }


        /**
          * GET: api/ApiAttr/ID=?
          * {HTTP}/ api/ApiAttr/ID=?
          * That methode return Attribute with ATTR_ID=id
          */
        [ResponseType(typeof(Models.Attribute))]
        public JsonResult GetAttribute(Guid id)
        {

            Models.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return null;
            }
            String[] table = new String[3];
            table[0] = attribute.ATTR_ID.ToString();
            table[1] = attribute.ATTR_Name.ToString();
            table[2] = attribute.ATTR_Value.ToString();

            JsonResult retData = new JsonResult();
            retData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            retData.MaxJsonLength = 1;
            retData.Data = table;
            return retData;
        }

       
        /**
        * PUT: api/ApiAttr/String Attr_Name ,String Attr_Value
        * {HTTP}/ api/ApiAttr/String Attr_Name ,String Attr_Value
        * That methode create Attribute in the database
        */
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAttribute(String Attr_Name ,String Attr_Value)
        {
            Models.Attribute attr = new Models.Attribute();
            attr.ATTR_ID = Guid.NewGuid();
            attr.ATTR_Name = Attr_Name;
            attr.ATTR_Value = Attr_Value;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Attributes.Add(attr);
        

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttributeExists(attr.ATTR_ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        /**
        * DELETE: api/ApeAttr/ID=?
        * {HTTP}/ api/ApeAttr/ID=?
        * That methode Delete Attribute with ATTR_ID=id
        */
        [ResponseType(typeof(Models.Attribute))]
        public IHttpActionResult DeleteAttribute(Guid id)
        {
            Models.Attribute attribute = db.Attributes.Find(id);
            if (attribute == null)
            {
                return NotFound();
            }

            db.Attributes.Remove(attribute);
            db.SaveChanges();

            return Ok(attribute);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttributeExists(Guid id)
        {
            return db.Attributes.Count(e => e.ATTR_ID == id) > 0;
        }
    }
}