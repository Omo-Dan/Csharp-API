using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartimentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            //this query is in raw format and not recommended.//to avoid sql injections //from tutor
            string query = @"
             select DepartimentId, DepartimentName from 
             dbo.Departiment
            ";
            DataTable table = new DataTable();
            using (var con= new SqlConnection(ConfigurationManager.ConnectionStrings["OmoAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        //POST method
        public string Post(Departiment dep)
        {
            try
            {
                string query = @"
                insert into dbo.Departiment values
                ('" + dep.DepartimentName + @"')
                
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
        public string Put(Departiment dep)
        {
            try
            {
                string query = @"
                update  dbo.Departiment set DepartimentName=
                '" + dep.DepartimentName + @"' where DepartimentId="+dep.DepartimentId+@"
                
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
                delete from  dbo.Departiment where DepartimentId=
                " + id + @"')
                
                ";
                
                return "Deleted Successfully!!";
            }
            catch (Exception)
            {
                return "Failed to delete";
            }
        }
    }
}
