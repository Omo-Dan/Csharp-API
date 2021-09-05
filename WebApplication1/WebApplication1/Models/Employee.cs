using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Employee
    {
        public int EmployeeId{get; set;}
        public string EmployeeName { get; set; }
        public string Departiment { get; set;  }
        public string DateOfJoining { get; set; }
        public string DateofJoining { get; internal set; }
        public string PhotoFileName { get; set; }

    }
}