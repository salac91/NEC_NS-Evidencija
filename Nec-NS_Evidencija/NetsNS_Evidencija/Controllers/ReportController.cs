using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEC_NS_Evidencija.Backend.DatabaseLayer;
using System.Data.SqlClient;
using NetsNS_Evidencija.Reports;
using System.Data;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using CrystalDecisions.Shared;
using NetsNS_Evidencija.Models;

namespace NetsNS_Evidencija.Controllers
{
    public class ReportController : Controller
    {

        [Authorize(Roles = "Mannager")]  
        public ActionResult ExportCoachReport(String id, String fromDate, String toDate)
        {
            ReportDocument rd = new ReportDocument();
            CoachDataSet ds = new CoachDataSet();
            ds.Tables[0].Merge(getCoachData(id, fromDate, toDate));
            rd.Load(Server.MapPath("~/Reports/CoachReport.rpt"));
            rd.SetDataSource(ds);

            rd.SetParameterValue("FromDate", fromDate);
            rd.SetParameterValue("ToDate", toDate);
            rd.SetParameterValue("Mannager", "Kruno Ostojić");
              
            rd.SetDatabaseLogon("sa", "salac91", @"SALAC-PC\SQLEXPRESS", "NEC-NS_DB");
             // Stop buffering the response
            Response.Buffer = false;
             // Clear the response content and headers
             Response.ClearContent();
             Response.ClearHeaders();
             Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
             stream.Seek(0, SeekOrigin.Begin);
             return File(stream, "application/pdf", "IzveštajTrener");
        }

        private DataTable getCoachData(string id,string dateFrom, string dateTo)
        {
          
            SqlConnection conn = new SqlConnection(@"data source=SALAC-PC\SQLEXPRESS;initial catalog=NEC-NS_DB;integrated security=True");
            SqlDataAdapter DA = new SqlDataAdapter("CoachProcedure", conn);
            DataTable DT = new DataTable();
            DA.SelectCommand.Parameters.AddWithValue("@COACH_INTERNAL_ID", id);
            DA.SelectCommand.Parameters.AddWithValue("@DateFrom", dateFrom);
            DA.SelectCommand.Parameters.AddWithValue("@DateTo", dateTo);
            DA.SelectCommand.CommandType = CommandType.StoredProcedure;
            conn.Open();
            DA.Fill(DT);
            conn.Close();
            return DT;
        }

        [Authorize(Roles = "Mannager")]
        public ActionResult ExportStudentReport(String id, String fromDate, String toDate, String name)
        {
            ReportDocument rd = new ReportDocument();
            StudentDataSet2 ds = new StudentDataSet2();
            ds.Tables[0].Merge(getStudentData(id, fromDate, toDate));
            rd.Load(Server.MapPath("~/Reports/StudentReport.rpt"));
            rd.SetDataSource(ds);

            rd.SetParameterValue("FromDate",fromDate);
            rd.SetParameterValue("ToDate",toDate);
            rd.SetParameterValue("Name",name);
           
            rd.SetDatabaseLogon("sa", "salac91", @"SALAC-PC\SQLEXPRESS", "NEC-NS_DB");
            // Stop buffering the response
            Response.Buffer = false;
            // Clear the response content and headers
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "IzveštajIgrač");
        }

        private DataTable getStudentData(string id, string dateFrom, string dateTo)
        {

            SqlConnection conn = new SqlConnection(@"data source=SALAC-PC\SQLEXPRESS;initial catalog=NEC-NS_DB;integrated security=True");
            SqlDataAdapter DA = new SqlDataAdapter("StudentProcedure", conn);
            DataTable DT = new DataTable();
            DA.SelectCommand.Parameters.AddWithValue("@STUDENT_INTERNAL_ID", id);
            DA.SelectCommand.Parameters.AddWithValue("@DateFrom", dateFrom);
            DA.SelectCommand.Parameters.AddWithValue("@DateTo", dateTo);
            DA.SelectCommand.CommandType = CommandType.StoredProcedure;
            conn.Open();
            DA.Fill(DT);
            conn.Close();
            return DT;
        }


        [Authorize(Roles = "Mannager")]
        public ActionResult ExportStudentTrainingReport(String id, String fromDate, String toDate)
        {
            ReportDocument rd = new ReportDocument();
            StudentDataSet ds = new StudentDataSet();
            ds.Tables[0].Merge(getStudentTrainingData(id, fromDate, toDate));
            rd.Load(Server.MapPath("~/Reports/StudentTrainingReport.rpt"));
            rd.SetDataSource(ds);

            rd.SetParameterValue("FromDate", fromDate);
            rd.SetParameterValue("ToDate", toDate);

            rd.SetDatabaseLogon("sa", "salac91", @"SALAC-PC\SQLEXPRESS", "NEC-NS_DB");
            // Stop buffering the response
            Response.Buffer = false;
            // Clear the response content and headers
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "IzveštajOdrađeniTreninzi");
        }

        private DataTable getStudentTrainingData(string id, string dateFrom, string dateTo)
        {

            SqlConnection conn = new SqlConnection(@"data source=SALAC-PC\SQLEXPRESS;initial catalog=NEC-NS_DB;integrated security=True");
            SqlDataAdapter DA = new SqlDataAdapter("StudentTrainingProcedure", conn);
            DataTable DT = new DataTable();
            DA.SelectCommand.Parameters.AddWithValue("@STUDENT_INTERNAL_ID", id);
            DA.SelectCommand.Parameters.AddWithValue("@DateFrom", dateFrom);
            DA.SelectCommand.Parameters.AddWithValue("@DateTo", dateTo);
            DA.SelectCommand.CommandType = CommandType.StoredProcedure;
            conn.Open();
            DA.Fill(DT);
            conn.Close();
            return DT;
        }

    }
}
