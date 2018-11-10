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
using System.Text;
using Newtonsoft.Json;
using System.Web.Mvc;

/**
 * WEB API CONTROLER
 * That controler used wen you call it 
 * and return Json Strings about the Employees and Attributes
 * you can call it from that linc:
 * {HTTP}/api/ApiData
 * {HTTP}/api/ApiData/ID=?
 */



namespace CiteApp_v1._2.Controllers
{
    public class ApiDataController : ApiController
    {
        private DBEntities db = new DBEntities();

       
        /**
         * GET: api/ApiData
         * {HTTP}/ api/ApiData
         * That methode return All Employees and their Attributes
         */
        public JsonResult Get()
        {
            var employees = db.Employees;
            //List<Models.AllDataViewer> listForAll = new List<Models.AllDataViewer>();
            if (employees != null)
            {
               
                DataTable dt = new DataTable();
                dt.Columns.Add("EMP_ID", typeof(string));
                dt.Columns.Add("EMP_Name", typeof(string));
                dt.Columns.Add("EMP_DateOfHire", typeof(string));
                dt.Columns.Add("EMP_Supervisor", typeof(string));
                dt.Columns.Add("ATTR_ID", typeof(string));
                dt.Columns.Add("ATTR_Name", typeof(string));
                dt.Columns.Add("ATTR_Value", typeof(string));

                foreach (Employee emp in employees)
                {
                    AllDataViewer empAttr = new AllDataViewer();
                    
                    foreach (Models.Attribute attr in emp.Attributes)
                    {
                        dt.Rows.Add(emp.EMP_ID.ToString(), emp.EMP_Name, emp.EMP_DateOfHire, emp.EMP_Supervisor, attr.ATTR_ID.ToString(), attr.ATTR_Name, attr.ATTR_Value);
                        
                    }
                }
                JsonResult retData = new JsonResult();
                retData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                retData.MaxJsonLength = dt.Rows.Count;
                retData.Data = dt;
                return retData;
            }
            return null;
        }

        
        /**
         * GET: api/ApiData/ID=?
         * {HTTP}/ api/ApiData
         * That methode return specific Employee and his Attributes
         */
        [ResponseType(typeof(Employee))]
        public JsonResult GetEmployee(Guid id)
        {
            
            var emp = db.Employees.Find(id);
            if (emp != null)
            {
               
               
                DataTable dt = new DataTable();
                
               
                dt.Columns.Add("EMP_ID");
                dt.Columns.Add("EMP_Name");
                dt.Columns.Add("EMP_DateOfHire");
                dt.Columns.Add("EMP_Supervisor");
                dt.Columns.Add("ATTR_ID");
                dt.Columns.Add("ATTR_Name");
                dt.Columns.Add("ATTR_Value");
               
                foreach (Models.Attribute attr in emp.Attributes)
                {
                    dt.Rows.Add(emp.EMP_ID, emp.EMP_Name , emp.EMP_DateOfHire ,emp.EMP_Supervisor , attr.ATTR_ID, attr.ATTR_Name , attr.ATTR_Value);
                }

                JsonResult retData = new JsonResult();
                retData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                retData.MaxJsonLength = dt.Rows.Count;
                retData.Data = dt;
                return retData;
            }
            
                return null;
        }
        
    }

}