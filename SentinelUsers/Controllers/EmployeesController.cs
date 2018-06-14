using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SentinelUsers.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employees> Get()
        {
            using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
            {
                return employeeDBEntities.Employees.ToList();
            }
        }

    }
}
