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
    [Authorized]
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

        [AllowAnonymous]
        public ActionResult Login(string CallBackMessage = null)
        {
            InitAdmin();
            if (!string.IsNullOrEmpty(CallBackMessage))
            {
                ViewBag.Message = CallBackMessage;
            }
            if (SystemSession.User != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string Username, string Password)
        {
            ModelState.Clear();
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

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(tbl_User m)
        {
            var user = new tbl_User();
            
            if (ModelState.IsValid)
            {
                if (user.Create(m))
                {
                    return RedirectToAction("Login", new { CallBackMessage = "Congratulation for making your official account!" });
                }
                ModelState.AddModelError("", "Username already exist!");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/");
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