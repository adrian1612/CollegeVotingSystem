using CollegeVotingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeVotingSystem.Models
{
    [Authorized]
    public class UserController : Controller
    {
        tbl_User mod = new tbl_User();
        public ActionResult Index()
        {
            var list = mod.List();
            return View(list);
        }

        public ActionResult Action(string Type, int? ID = null)
        {
            switch (Type)
            {
                case "Add":
                    return RedirectToAction("Create");
                case "Edit":
                    return RedirectToAction("Edit", new { ID = ID });
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_User m)
        {
            if (ModelState.IsValid)
            {
                mod.Create(m);
                return RedirectToAction("Index");
            }
            return View(m);
        }

        public ActionResult Edit(int ID)
        {
            var item = mod.Find(ID);
            return View(item);
        }

        [ActionName("Profile")]
        public ActionResult UserProfile(string CallBackMessage = null)
        {
            var item = mod.Find(SystemSession.User.ID);
            if (!string.IsNullOrEmpty(CallBackMessage))
            {
                ViewBag.Message = CallBackMessage;
            }
            return View(item);
        }

        [ActionName("Profile")]
        [HttpPost]
        public ActionResult UserProfile(tbl_User m)
        {
            if (ModelState.IsValid)
            {
                mod.Update(m);
                Session["User"] = mod.Find(m.ID);
                return RedirectToAction("Profile", new { CallBackMessage = "Information successfully updated!" });
            }
            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(tbl_User m)
        {
            if (ModelState.IsValid)
            {
                mod.Update(m);
                return RedirectToAction("Index");
            }
            return View(m);
        }

        public ActionResult Detail(int ID)
        {
            var item = mod.Find(ID);
            return View(item);
        }

        public ActionResult Delete(int ID)
        {
            var item = mod.Find(ID);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(tbl_User m)
        {
            m.Delete(m);
            return RedirectToAction("Index");
        }
    }
}