using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarrerLib;

namespace onlinecarrer.Controllers
{
    public class JS_ProfessionalController : Controller
    {
        private CareerEntities db = new CareerEntities();

        // GET: JS_Professional
        public ActionResult Index()
        {
            var jobSeeker_Professional = db.JobSeeker_Professional.Include(j => j.Job_Seeker);
            return View(jobSeeker_Professional.ToList());
        }

        // GET: JS_Professional/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobSeeker_Professional jobSeeker_Professional = db.JobSeeker_Professional.Find(id);
            if (jobSeeker_Professional == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker_Professional);
        }

        // GET: JS_Professional/Create
       
        public ActionResult Create(int id)
        {
            Session["jsid"] = id;
            //int jsid = Convert.ToInt32(TempData["name"]);
            return View();
        }
        
        // POST: JS_Professional/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "jsID,jsKeySkills,jsPreferredIndustry,jsYearOfExperience,jsPrevcompName,jsPreviousTitile,JsPrevJobDescription")] JobSeeker_Professional jobSeeker_Professional)
        {
            if (ModelState.IsValid)
            {
                jobSeeker_Professional.jsID =Convert.ToInt32( Session["jsid"]);
                db.JobSeeker_Professional.Add(jobSeeker_Professional);
                db.SaveChanges(); 
                
                return RedirectToAction("LoginJSVal", "Job_Seeker_Reg");
            }

            ViewBag.jsID = new SelectList(db.Job_Seeker, "jsID", "jsFullName", Session["jsid"]);
            return View(jobSeeker_Professional);
        }

        // GET: JS_Professional/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobSeeker_Professional jobSeeker_Professional = db.JobSeeker_Professional.Find(id);
            if (jobSeeker_Professional == null)
            {
                return HttpNotFound();
            }
            ViewBag.jsID = new SelectList(db.Job_Seeker, "jsID", "jsFullName", jobSeeker_Professional.jsID);
            return View(jobSeeker_Professional);
        }

        // POST: JS_Professional/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jsProfID,jsID,jsKeySkills,jsPreferredIndustry,jsYearOfExperience,jsPrevcompName,jsPreviousTitile,JsPrevJobDescription")] JobSeeker_Professional jobSeeker_Professional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobSeeker_Professional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jsID = new SelectList(db.Job_Seeker, "jsID", "jsFullName", jobSeeker_Professional.jsID);
            return View(jobSeeker_Professional);
        }

        // GET: JS_Professional/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobSeeker_Professional jobSeeker_Professional = db.JobSeeker_Professional.Find(id);
            if (jobSeeker_Professional == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker_Professional);
        }

        // POST: JS_Professional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobSeeker_Professional jobSeeker_Professional = db.JobSeeker_Professional.Find(id);
            db.JobSeeker_Professional.Remove(jobSeeker_Professional);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
