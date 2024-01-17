using CollegeVotingSystem.Models;
using SourceAFIS.Simple;
using SourceAFIS.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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

        public ActionResult UpdateUserBiometric(int ID, string cb = null)
        {
            var item = mod.Find(ID);
            if (!string.IsNullOrEmpty(cb))
            {
                ViewBag.Message = cb;
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult UpdateUserBiometric(tbl_User m)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                mod.UpdateUserBiometric(m);
                return RedirectToAction("UpdateUserBiometric", new { ID = m.ID, cb = "User biometric successfully saved!" });
            }
            return View(m);
        }

        [HttpPost]
        public ActionResult Identify(string base64)
        {
            var fingerprint = new Fingerprint();
            fingerprint.AsBitmap = new System.Drawing.Bitmap(new MemoryStream(Convert.FromBase64String(base64)));
            return Json(mod.Find(mod.Identify(fingerprint).Id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult IdentifyPerson()
        {
            return View();
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