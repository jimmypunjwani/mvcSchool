using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcSchool.Models;

namespace mvcSchool.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    public class StudentsController : Controller
    {
        private mvcSchool_DBEntities db = new mvcSchool_DBEntities();
        
        

        public StudentsController()
        {
        }


        //Adding Controller for the partial view (Async doesnot support partial view):
        public PartialViewResult _studentPartial(int? studid)
        {
            var enrollments = db.Enrollments
                .Where(q => q.StudentID == studid)
                .Include(e => e.Course)
                .Include(e => e.Student);
            return PartialView(enrollments.ToList());
        }

        

        // GET: Students
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            return View(await db.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentID,LastName,FirstName,EnrollmentDate,Hobby,MiddleName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }


        //Adding New Function for AddStudent which we have refered in Create View:
        //[HttpPost]
        //public async Task<JsonResult> AddStudent([Bind(Include = "CourseID,StudentID")] Enrollment enrollment)
        //{
        //    return (new EnrollmentsController()).AddStudent(enrollment);
        //}



        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            //ViewBag.StudentCourse = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentID,LastName,FirstName,EnrollmentDate,Hobby,MiddleName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
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
