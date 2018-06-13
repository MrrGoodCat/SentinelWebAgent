﻿using SentinelDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SentinelWebAgent
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities())
            {
                return employeeDBEntities.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
        
    }
}