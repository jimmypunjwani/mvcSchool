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
    [Authorize(Roles = "Admin,Supervisor,Teacher")]
    public class EnrollmentsController : Controller
    {
        private mvcSchool_DBEntities db = new mvcSchool_DBEntities();

        // GET: Enrollments
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Lecturer).Include(e => e.Student);
            return View(await enrollments.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.LecturerID = new SelectList(db.Lecturers, "LecturerID", "FirstName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "LastName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID,LecturerID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.LecturerID = new SelectList(db.Lecturers, "LecturerID", "FirstName", enrollment.LecturerID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }

        //Adding New Function for AddStudent which we have refered in Create View:
        [HttpPost]
        public async Task<JsonResult> AddStudent([Bind(Include = "CourseID,StudentID")] Enrollment enrollment)
        {
            try
            {
                //Checking if Student is already enrolled in the particular Course:
                var IsEnrolled = db.Enrollments.Any(q => q.CourseID == enrollment.CourseID && q.StudentID == enrollment.StudentID);
                //Checking both conditions (IsEnrolled == false is similar to !IsEnrolled):
                if (ModelState.IsValid && !IsEnrolled)
                {
                    db.Enrollments.Add(enrollment);
                    await db.SaveChangesAsync();
                    return Json(new { IsSuccess = true, Message = "Student Added Successfully."}, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, Message = "Student Already Enrolled or Not Entered." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { IsSuccess = false, Message = "Please Contact Your Administartor." }, JsonRequestBehavior.AllowGet);
            }
        }



        // GET: Enrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.LecturerID = new SelectList(db.Lecturers, "LecturerID", "FirstName", enrollment.LecturerID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollmentID,Grade,CourseID,StudentID,LecturerID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.LecturerID = new SelectList(db.Lecturers, "LecturerID", "FirstName", enrollment.LecturerID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            db.Enrollments.Remove(enrollment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Adding Function of autocomplete for Student Name:
        [HttpPost]
        public JsonResult GetStudents(string term)
        {
            var students = db.Students.Select(q => new {
                FullName = q.FirstName + " " + q.LastName,
                Id = q.StudentID
            }).Where(q => q.FullName.Contains(term));
            return Json(students, JsonRequestBehavior.AllowGet);
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
