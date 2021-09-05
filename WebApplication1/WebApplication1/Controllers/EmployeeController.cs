using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Configuration;
using System.Web;

namespace WebApplication1.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            //this query is in raw format and not recommended.//to avoid sql injections //from tutor
            string query = @"
             select EmployeeId, EmployeeName, Departiment, convert(varchar(10), DateOfJoining,120) as DateOfJoining, 
             PhotoFileName from 
             dbo.Employee
            ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["OmoAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //POST method
        public string Post(Employee emp)
        {
            try
            {
                string query = @"
                insert into dbo.Employee values
                (
                 '" + emp.EmployeeName + @"'
                 ,'" + emp.Departiment + @"'
                 ,'" + emp.DateofJoining + @"'
                  ,'" + emp.PhotoFileName + @"'
                )
                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["OmoAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully!!";
            }
            catch (Exception)
            {
                return "Failed to add";
            }
        }

        //PUT method
        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                update  dbo.Employee set 
               EmployeeName=
                '" + emp.EmployeeName + @"' ,
               Departiment=
                '" + emp.Departiment + @"' ,
              DateOfJoining=
                '" + emp.DateOfJoining + @"', 
              PhotoFileName=
                '" + emp.PhotoFileName + @"' 


                 where EmployeeId=" + emp.EmployeeId + @"
                
                ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["OmoAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully!!";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }

        //Delete method
        public string Delete(int id)
        {
            try
            {
                string query = @"
                delete from  dbo.Employee EmployeeId=
                " + id + @"')
                
                ";

                return "Deleted Successfully!!";
            }
            catch (Exception)
            {
                return "Failed to delete";
            }
        }

        
        //custom method
        [Route ("api/Employee/GetAllDepartimentName")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartimentNames()
        {
            string query = @"
                select DepartimentName from  dbo.Departiment
                
                ";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["OmoAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("api/Employee/SaveFile")]
        //methods for saving files in Photos folder

        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var pytsicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                postedFile.SaveAs(pytsicalPath);
                return filename;
            }
            catch(Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
 