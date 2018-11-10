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
using Microsoft.AspNet;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


/**
 * WEB API CONTROLER
 * That controler used wen you call it 
 * and return Json Strings about the Employees
 * you can call it from that linc:
 * {HTTP}/api/ApiEmp
 * {HTTP}/api/ApiEmp/ID=?
 * {HTTP}/api/ApiEmp/String EMP_Name,DateTime EMP_EMP_DateOfHire ,Guid idSuper * 
 */





namespace CiteApp_v1._2.Controllers
{
    public class ApiEmpController : ApiController
    {
        private DBEntities db = new DBEntities();


        /**
         * GET: api/ApiEmp
         * {HTTP}/ api/ApiEmp
         * That methode return All Employees
         */
        [System.Web.Http.HttpGet]
        public JsonResult Get()
        {
            var employees = db.Employees;
            String [,] table =new String[employees.Count(),4];
            int i = 0;
           foreach(Employee emp in employees)
            {
                table[i, 0] = emp.EMP_ID.ToString();
                table[i, 1] = emp.EMP_Name.ToString();
                table[i, 2] = emp.EMP_DateOfHire.ToString();
                table[i, 3] = emp.EMP_Supervisor.ToString();
                i++;
            }

            JsonResult retData = new JsonResult();
            retData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            retData.MaxJsonLength = employees.Count();
            retData.Data = table;
            return retData;
           
        }

        /**
         * GET: api/ApiEmp/ID=?
         * {HTTP}/ api/ApiEmp/ID=?
         * That methode return Employee with EMP_ID=id
         */
        [System.Web.Http.HttpGet]
        public JsonResult GetEmployee(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return null;
            }
            String[,] table = new String[1, 4];
            table[0, 0] = employee.EMP_ID.ToString();
            table[0, 1] = employee.EMP_Name.ToString();
            table[0, 2] = employee.EMP_DateOfHire.ToString();
            table[0, 3] = employee.EMP_Supervisor.ToString();



            JsonResult retData = new JsonResult();
            retData.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            retData.MaxJsonLength = 1;
            retData.Data = table;
            return retData;
        }

       
        /**
         * PUT: api/ApiEmp/String EMP_Name,DateTime EMP_EMP_DateOfHire ,Guid idSuper
         * {HTTP}/ api/ApiEmp/String EMP_Name,DateTime EMP_EMP_DateOfHire ,Guid idSuper
         * That methode create Employee in the database
         */
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(String EMP_Name,DateTime EMP_EMP_DateOfHire ,Guid idSuper)
        {
            Employee employee = new Employee();
            employee.EMP_ID= Guid.NewGuid();
            employee.EMP_Name = EMP_Name;
            employee.EMP_DateOfHire = EMP_EMP_DateOfHire;
            employee.EMP_Supervisor = idSuper;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.EMP_ID))
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
         * DELETE: api/ApiEmp/ID=?
         * {HTTP}/ api/ApiEmp/ID=?
         * That methode Delete Employee with EMP_ID=id
         */
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        private bool EmployeeExists(Guid id)
        {
            return db.Employees.Count(e => e.EMP_ID == id) > 0;
        }
    }
}