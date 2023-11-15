using CollegeVotingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollegeVotingSystem.Models
{
    public class ElectionController : Controller
    {
        tbl_Election mod = new tbl_Election();
        public ActionResult Index()
        {
            var list = mod.List();
            return View(list);
        }

        public ActionResult ElectionVote()
        {
            var item = mod.Election();
            if (item == null)
            {
                ViewBag.MyVote = mod.MyVote();
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult ElectionVote(tbl_Election m, Guid[] Positions)
        {
            ModelState.Clear();
            var position = from i in Positions
                           where i != Guid.Empty
                           select new Vote(m.ID, SystemSession.User.ID, i);
            if (ModelState.IsValid) 
            {
                mod.Vote(position.ToList());
                return RedirectToAction("ElectionVote");
            }
            var item = mod.Election();
            return View(item);
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

        public JsonResult Users()
        {
            var users = new tbl_User().List();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Positions()
        {
            var positions = new tbl_Position().List();
            return Json(positions, JsonRequestBehavior.AllowGet);
        }

        void CandidateValidation(tbl_Election m)
        {
            if (m.Candidates?.Count <= 0)
            {
                ModelState.AddModelError("", "Need atleast 1 candidate to continue.");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_Election m)
        {
            CandidateValidation(m);
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

        [HttpPost]
        public ActionResult Edit(tbl_Election m)
        {
            CandidateValidation(m);
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
        public ActionResult Delete(tbl_Election m)
        {
            m.Delete(m);
            return RedirectToAction("Index");
        }
    }
}