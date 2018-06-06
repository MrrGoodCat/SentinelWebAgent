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


        public Employee Get(int id)
        {
            using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
            {
                return employeeDBEntities.Employees.FirstOrDefault(e => e.ID == id);
            }
        }
    }
}
