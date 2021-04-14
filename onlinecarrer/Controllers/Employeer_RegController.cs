using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CarrerLib;
using onlinecarrer.Models;

namespace onlinecarrer.Controllers
{
  
    public class Employeer_RegController : Controller
    {
        private CareerEntities db = new CareerEntities();

        // GET: Employeer_Reg
        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        // GET: Employeer_Reg/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Employeer_Reg/Create
       
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employeer_Reg/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "compNamae,compBranch,compAddress,compCity,compState,compEmail,compPhone,compPassword,compUsername")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("LoginEmpVal");
            }

            return View(company);
        }

        // GET: Employeer_Reg/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Employeer_Reg/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "compId,compNamae,compBranch,compAddress,compCity,compState,compEmail,compPhone,compPassword,compUsername")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Employeer_Reg/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Employeer_Reg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
       
        public ActionResult LoginEmpVal()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LoginEmpVal(Companylogin c)
        {
            using (var context = new CareerEntities())
            {
                bool isvalid = context.Companies.Any(x => x.compId == c.comapnyid && x.compPassword == c.compPassword);
                
                if (isvalid)
                {
                    Session["eid"] = c.comapnyid;
                    FormsAuthentication.SetAuthCookie(c.comapnyid.ToString(), false);
                    return RedirectToAction("Afterlogin","EmpAfterLogin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");

                }

            }
            return View();
            


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Welcome", "WelcomeHome");
        }
       
    }
}
