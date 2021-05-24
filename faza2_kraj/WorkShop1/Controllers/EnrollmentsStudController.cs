﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkShop1.Data;
using WorkShop1.Models;
using WorkShop1.ViewModels;

namespace WorkShop1.Controllers
{
    public class EnrollmentsStudController : Controller
    {
        private readonly WorkShop1Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EnrollmentsStudController(WorkShop1Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        /*public IActionResult EditByStudent(long id)
        {
            var enrollment = _context.Enrollment.FindAsync(id);

            var vm = new EnrollmentView
            {

            };

            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Title", enrollment.);
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentId", "FirstName", enrollment.StudentID);
            return View(vm);
        }*/

        /*public IActionResult EditByStudent(long id)
        {
           var vm = new EnrollmentView {
               EnrollmentID = id,
            };
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> EditByStudent(long id, EnrollmentView vm)
        {

            if (ModelState.IsValid)
            {
               
                    string uniqueFileName = UploadedFile(vm);
                    Enrollment enrollment = new Enrollment
                    {
                        EnrollmentID = vm.EnrollmentID,
                        Semester = vm.Semester,
                        Year = vm.Year,
                        Grade = vm.Grade,
                        SeminalUrl = uniqueFileName,
                        ProjectUrl = vm.ProjectUrl,
                        SeminalPoints = vm.SeminalPoints,
                        ProjectPoints = vm.ProjectPoints,
                        AdditionalPoints = vm.AdditionalPoints,
                        ExamPoints = vm.ExamPoints,
                        FinishDate = vm.FinishDate,
                        CourseID = vm.CourseID,
                        StudentID = vm.StudentID
                    };

                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseID", "Title", vm.CourseID);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", vm.StudentID);
            return View();
        }


        private string UploadedFile(EnrollmentView model)
        {
            string uniqueFileName = null;

            if (model.SeminalUrl != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.SeminalUrl.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.SeminalUrl.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }*/

        /*public async Task<IActionResult> EditByStudent(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseID);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", enrollment.StudentID);

            Enrollment Vmodel = new Enrollment
            {
                EnrollmentID = enrollment.EnrollmentID,
                Semester = enrollment.Semester,
                Year = enrollment.Year,
                Grade = enrollment.Grade,
                ProjectUrl = enrollment.ProjectUrl,
                SeminalPoints = enrollment.SeminalPoints,
                ProjectPoints = enrollment.ProjectPoints,
                AdditionalPoints = enrollment.AdditionalPoints,
                ExamPoints = enrollment.ExamPoints,
                FinishDate = enrollment.FinishDate,
                CourseID = enrollment.CourseID,
                StudentID = enrollment.StudentID,
            };

            ViewData["StudentFullName"] = _context.Student.Where(s => s.ID == enrollment.StudentID).Select(s => s.FullName).FirstOrDefault();
            ViewData["CourseTitle"] = _context.Course.Where(s => s.CourseID == enrollment.CourseID).Select(s => s.Title).FirstOrDefault();
            return View(Vmodel);
        }

        // POST: Enrollments/EditByStudent/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditByStudent(int id, EnrollmentView Vmodel)
        {
            if (id != Vmodel.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(Vmodel);

                    Enrollment enrollment = new Enrollment
                    {
                        EnrollmentID = Vmodel.EnrollmentID,
                        Semester = Vmodel.Semester,
                        Year = Vmodel.Year,
                        Grade = Vmodel.Grade,
                        SeminalUrl = uniqueFileName,
                        ProjectUrl = Vmodel.ProjectUrl,
                        SeminalPoints = Vmodel.SeminalPoints,
                        ProjectPoints = Vmodel.ProjectPoints,
                        AdditionalPoints = Vmodel.AdditionalPoints,
                        ExamPoints = Vmodel.ExamPoints,
                        FinishDate = Vmodel.FinishDate,
                        CourseID = Vmodel.CourseID,
                        StudentID = Vmodel.StudentID,
                    };

                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(Vmodel.EnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = Vmodel.EnrollmentID });
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", Vmodel.CourseID);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", Vmodel.StudentID);
            return View(Vmodel);
        }
        private string UploadedFile(EnrollmentView Vmodel)
        {
            string uniqueFileName = null;

            if (Vmodel.SeminalUrl != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Vmodel.SeminalUrl.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Vmodel.SeminalUrl.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }*/

        public async Task<IActionResult> EditByStudent(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "CourseID", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID", enrollment.StudentID);
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditByStudent(long id, [Bind("EnrollmentID,CourseID,StudentID,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "CourseID", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID", enrollment.StudentID);
            return View(enrollment);
        }


        // GET: EnrollmentsStud
        public async Task<IActionResult> Index()
        {
            var workShop1Context = _context.Enrollment.Include(e => e.Course).Include(e => e.Student);
            return View(await workShop1Context.ToListAsync());
        }

        // GET: EnrollmentsStud/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: EnrollmentsStud/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Title");
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FirstName");
            return View();
        }

        // POST: EnrollmentsStud/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentID,CourseID,StudentID,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: EnrollmentsStud/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: EnrollmentsStud/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EnrollmentID,CourseID,StudentID,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: EnrollmentsStud/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: EnrollmentsStud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(long id)
        {
            return _context.Enrollment.Any(e => e.EnrollmentID == id);
        }
    }
}
