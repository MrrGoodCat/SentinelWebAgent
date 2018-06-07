using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SentinelDataAccess;

namespace SentinelWebAgent.Controllers
{
    public class SentinelDataController : ApiController
    {

        public IEnumerable<Employee> Get()
        {
            using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
            {
                return employeeDBEntities.Employees.ToList();
            }
        }


        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
            {
                
                var employee = employeeDBEntities.Employees.FirstOrDefault(e => e.ID == id);
                if (employee != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID: " + id + " not found.");
                }
            }
        }

        public HttpResponseMessage Post ([FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
                {
                    employeeDBEntities.Employees.Add(employee);
                    employeeDBEntities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }            
        }

        public HttpResponseMessage Delete (int id)
        {
            try
            {
                using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
                {
                    var result = employeeDBEntities.Employees.FirstOrDefault(e => e.ID == id);

                    if (result == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id + " not found to delete.");
                    }
                    else
                    {
                        employeeDBEntities.Employees.Remove(result);
                        employeeDBEntities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
                {
                    var result = employeeDBEntities.Employees.FirstOrDefault(e => e.ID == id);

                    if (result == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id + " not found to update.");
                    }
                    else
                    {
                        result.FirstName = employee.FirstName;
                        result.LastName = employee.LastName;
                        result.Gender = employee.Gender;
                        result.Salary = employee.Salary;
                        employeeDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
