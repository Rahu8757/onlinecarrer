using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CarrerLib;
using onlinecarrer.Models;

namespace onlinecarrer.Controllers
{
    
    public class Job_Seeker_RegController : Controller
    {
        private CareerEntities db = new CareerEntities();
        
        
        // GET: Job_Seeker_Reg
        public ActionResult Index()
        {
            
            return View(db.Job_Seeker.ToList());
        }

        // GET: Job_Seeker_Reg/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Seeker job_Seeker = db.Job_Seeker.Find(id);
            if (job_Seeker == null)
            {
                return HttpNotFound();
            }
            return View(job_Seeker);
        }

        // GET: Job_Seeker_Reg/Create
       
        public ActionResult Create()
        {
            return View();
        }

        // POST: Job_Seeker_Reg/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "jsFullName,jsEmail,jsPassword,jsPhone,jsDOB,jsCity,jsState,jsMartialStatus,jsLanguagePreferred,jsResume,jsMajor,jsUniversity,jsInstitute,jsYearOfGraduation")] Job_Seeker job_Seeker, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("CustomError", "Please select CV");
                return View();
            }

            if (!(file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                file.ContentType == "application/pdf"))
            {
                ModelState.AddModelError("CustomError", "Only .docx and .pdf file allowed");
                return View();
            }
            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                fileName=job_Seeker.jsFullName.ToString()+Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/cv_upload"), fileName));


                job_Seeker.jsResume= fileName;
                db.Job_Seeker.Add(job_Seeker);
                    
                    db.SaveChanges();
                
                //ModelState.Clear();
               
                //ViewBag.Message = "Successfully Done";
                //db.Job_Seeker.Add(job_Seeker);
              
                //db.SaveChanges();
               // Session["name"] = job_Seeker.jsID;
                return RedirectToAction("Create","JS_Professional",new {id= job_Seeker.jsID });
            }

            return View(job_Seeker);
        }

        // GET: Job_Seeker_Reg/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Seeker job_Seeker = db.Job_Seeker.Find(id);
            if (job_Seeker == null)
            {
                return HttpNotFound();
            }
            return View(job_Seeker);
        }

        // POST: Job_Seeker_Reg/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jsID,jsFullName,jsEmail,jsPassword,jsPhone,jsDOB,jsCity,jsState,jsMartialStatus,jsLanguagePreferred,jsResume,jsMajor,jsUniversity,jsInstitute,jsYearOfGraduation")] Job_Seeker job_Seeker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job_Seeker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job_Seeker);
        }

        // GET: Job_Seeker_Reg/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Seeker job_Seeker = db.Job_Seeker.Find(id);
            if (job_Seeker == null)
            {
                return HttpNotFound();
            }
            return View(job_Seeker);
        }

        // POST: Job_Seeker_Reg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job_Seeker job_Seeker = db.Job_Seeker.Find(id);
            db.Job_Seeker.Remove(job_Seeker);
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
      
        public ActionResult LoginJSVal()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult LoginJSVal(Jobseekerlogin q)
        {
            TempData["name"] = q.JsName;
            using (var context = new CareerEntities())
            {
                bool isvalid = context.Job_Seeker.Any(x => x.jsFullName == q.JsName && x.jsPassword == q.JsPassword);
                if (isvalid)
                {
                    Session["name"] = q.JsName;
                    FormsAuthentication.SetAuthCookie(q.JsName, false);
                    return RedirectToAction("Availablejob", "Jobs_Posted");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username  or Password");

                }

            }
            return View();

        }
        public ActionResult AppliedjobbyJs()
        {
            string jsName = Session["name"].ToString();
            var tbs = db.seeAppliedJobs(jsName);
            if (tbs.Count()!=0)
            {
                List<seeAppliedJobs_Result> li = tbs.ToList();
                List<SeeAppliedJobmodal> sl = new List<SeeAppliedJobmodal>();
                foreach (var item in li)
                {
                    SeeAppliedJobmodal sam = new SeeAppliedJobmodal();
                    sam.JobId = item.Jobid;
                    sam.Companyname = item.compNamae;
                    sam.Jobdescrp = item.jobDescription;
                    sam.Jobindustry = item.jobIndustry;
                    sam.Jobkeyskills = item.jobKeySkills;
                    sam.Jobtitle = item.jobTitle;
                    sam.Appdate = item.AppliedDate;
                    
                    sl.Add(sam);
                }

                return View(sl);
            }
            else
            {
                return View("Applynotfound");
            }
        }
    }
}
