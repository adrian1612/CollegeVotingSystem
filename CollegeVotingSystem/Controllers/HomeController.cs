using CollegeVotingSystem.Classes;
using CollegeVotingSystem.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeVotingSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RestrictedAccess()
        {
            return View();
        }

        public ActionResult Login(string CallBackMessage = null)
        {
            if (!string.IsNullOrEmpty(CallBackMessage))
            {
                ViewBag.Message = CallBackMessage;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            InitAdmin();
            var user = new tbl_User();
            if (ModelState.IsValid)
            {
                var User = user.Login(Username, Password);
                if (User != null)
                {
                    Session["User"] = User;
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
            }
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(tbl_User m)
        {
            var user = new tbl_User();
            if (ModelState.IsValid)
            {
                user.Create(m);
                return RedirectToAction("Login", new { CallBackMessage = "Congratulation for making your official account!" });
            }
            return View();
        }


        void InitAdmin()
        {
            if (Debugger.IsAttached)
            {
                var s = new dbcontrol();
                var count = 0;
                s.Query("SELECT COUNT(*) FROM tbl_User").ForEach(r =>
                {
                    count = Convert.ToInt32(r[0]);
                });
                if (count <= 0)
                {
                    s.Query("INSERT INTO tbl_User ([Username],[Password],[Role],[Active]) VALUES ('admin', 'admin', 1, 1)");
                }
            }
        }

        //public ActionResult Print(int ID)
        //{
        //    ReportViewer rv = new ReportViewer(); //EXCELOPENXML FOR EXCEL & application/vnd.ms-excel FOR contentType
        //    rv.LocalReport.ReportPath = Server.MapPath("~/Reports/Report.rdlc");
        //    var data = new object { };
        //    rv.LocalReport.DataSources.Add(new ReportDataSource("Report", data));
        //    var file = rv.LocalReport.Render("pdf");
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", $"inline;filename=Form.pdf");
        //    Response.Buffer = true;
        //    Response.Clear();
        //    Response.BinaryWrite(file);
        //    Response.End();
        //    return View(file);
        //}
    }
}