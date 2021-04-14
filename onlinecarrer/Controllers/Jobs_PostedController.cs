using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarrerLib;
using onlinecarrer.Models;
using System.Data.Entity.Core.Objects;


namespace onlinecarrer.Controllers
{
    public class Jobs_PostedController : Controller
    {
        private CareerEntities db = new CareerEntities();

        // GET: Jobs_Posted
        public ActionResult Index()
        {
            var jobs_Posted = db.Jobs_Posted.Include(j => j.Company);
            return View(jobs_Posted.ToList());
        }

        // GET: Jobs_Posted/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs_Posted jobs_Posted = db.Jobs_Posted.Find(id);
            if (jobs_Posted == null)
            {
                return HttpNotFound();
            }
            return View(jobs_Posted);
        }

        // GET: Jobs_Posted/Create
        public ActionResult Create()
        {
           Session["neid"]=Convert.ToInt32( Session["eid"]);
            //ViewBag.companyID = new SelectList(db.Companies, "compId", "compNamae");
            return View();
        }

        // POST: Jobs_Posted/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "companyID,jobIndustry,jobKeySkills,JobTitle,JobDescription")] Jobs_Posted jobs_Posted)
        {
            if (ModelState.IsValid)
            {
                jobs_Posted.companyID = Convert.ToInt32(Session["neid"]);
                db.Jobs_Posted.Add(jobs_Posted);
                db.SaveChanges();
                return RedirectToAction("Afterlogin", "EmpAfterLogin");
            }

            ViewBag.companyID = new SelectList(db.Companies, "compId", "compNamae", Session["neid"]);
            return View(jobs_Posted);
        }

        // GET: Jobs_Posted/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs_Posted jobs_Posted = db.Jobs_Posted.Find(id);
            if (jobs_Posted == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyID = new SelectList(db.Companies, "compId", "compNamae", jobs_Posted.companyID);
            return View(jobs_Posted);
        }

        // POST: Jobs_Posted/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,companyID,jobIndustry,jobKeySkills,JobTitle,JobDescription")] Jobs_Posted jobs_Posted)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobs_Posted).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.companyID = new SelectList(db.Companies, "compId", "compNamae", jobs_Posted.companyID);
            return View(jobs_Posted);
        }

        // GET: Jobs_Posted/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs_Posted jobs_Posted = db.Jobs_Posted.Find(id);
            if (jobs_Posted == null)
            {
                return HttpNotFound();
            }
            return View(jobs_Posted);
        }

        // POST: Jobs_Posted/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jobs_Posted jobs_Posted = db.Jobs_Posted.Find(id);
            db.Jobs_Posted.Remove(jobs_Posted);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Availablejob()
        {
            var jobs_Posted = db.Jobs_Posted.Include(j => j.Company);
            return View(jobs_Posted.ToList());
        }
        public ActionResult Joblist(int? id,int nm)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs_Posted jobs_Posted = db.Jobs_Posted.Find(id);
            if (jobs_Posted == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyID = new SelectList(db.Companies, "compId", "compNamae", jobs_Posted.companyID);
            return View(jobs_Posted);
        }
        [HttpPost]
        public ActionResult Joblist(int? id,int nm,string c)
        {
            //id = Convert.ToInt32(Request.QueryString["JobId"]);
            string s = Session["name"].ToString();
            //Careerdal cd = new Careerdal();
            //ApJob aa = new ApJob();
            //aa.Id =Convert.ToInt32( id);
            //aa.Name = s;
            //aa.ApDate = DateTime.Now;
            //cd.Insertjobapplied(aa);
            db.addJobsApplied(s, id, DateTime.Now,nm);


            return RedirectToAction("Availablejob");
        }
        public ActionResult Searchjob()
        {
            string nm = Session["name"].ToString();

            ObjectParameter keyskills = new ObjectParameter("keyskills",typeof(string));
            var returnValue = db.findkeyskills(nm,keyskills);
            string s = keyskills.Value.ToString();
            var tbs = db.searchByKeySkills(s);
            if (tbs.Count() != 0)
            {
                List<searchByKeySkills_Result> li = tbs.ToList();
                List<Searchjobmodal> lsm = new List<Searchjobmodal>();
                foreach (var item in li)
                {
                    Searchjobmodal sm = new Searchjobmodal();
                    sm.JobIndustry = item.jobIndustry;
                    sm.JobDescription = item.jobDescription;
                    sm.JobKeySkills = item.jobKeySkills;
                    sm.JobTitle = item.jobTitle;
                   
                    //sm.JobId = item.jobId;
                    lsm.Add(sm);
                }

                return View(lsm);
            }
            else
            {
                return View("Nomatchjobfound");
            }
        }
        public ActionResult Seepostedjob()
        {
            int eid = Convert.ToInt32(Session["eid"]);
            
            var postdt = db.seePostedJobs(eid);
            if (postdt.Count() != 0)
            {
                List<seePostedJobs_Result> li = postdt.ToList();
                List<Emppostjobdt> Epjml = new List<Emppostjobdt>();
                foreach (var item in li)
                {
                    Emppostjobdt Epm = new Emppostjobdt();
                    Epm.Jobid = item.JobId;
                    Epm.JobIndustry = item.jobIndustry;
                    Epm.Jobdescription= item.JobDescription;
                    Epm.Jobkeyskills = item.jobKeySkills;
                    Epm.JobTitle = item.JobTitle;
                    //sm.JobId = item.jobId;
                    Epjml.Add(Epm);
                }

                return View(Epjml);
            }
            else
            {
                return View("errnojobpos");
            }
            
        }
        public ActionResult SearchEmployee()
        {
            int i = Convert.ToInt32(Session["eid"]);
            var sedt = db.Applyjsdetails(i);
            
            if (sedt.Count() != 0)
            {
                List<Applyjsdetails_Result> li = sedt.ToList();
                
                List<SeeEmpapply> SEpjml = new List<SeeEmpapply>();
                foreach (var item in li)
                {
                    SeeEmpapply SEpm = new SeeEmpapply();
                    SEpm.Jsfullname = item.jsFullName;
                    SEpm.Jsdob = item.jsDOB;
                    SEpm.Jsemail = item.jsEmail;
                    SEpm.Jslanguage = item.jsLanguagePreferred;
                    SEpm.Jsmartial = item.jsMartialStatus;
                    SEpm.Jsprefeeredind = item.jsPreferredIndustry;
                    SEpm.Jspreviousname = item.jsPrevcompName;
                    SEpm.Jsprevjobdesc = item.JsPrevJobDescription;
                    SEpm.Jsuniversity = item.jsUniversity;
                    SEpm.Jsyoe = item.jsYearOfGraduation;
                    SEpm.Jsyog = item.jsYearOfGraduation;
                    SEpjml.Add(SEpm);


                }

                return View(SEpjml);
            }
            else
            {
                return View("errnohasapply");
            }

           
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
