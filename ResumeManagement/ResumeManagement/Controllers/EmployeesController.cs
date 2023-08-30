using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResumeManagement.Models;
using ResumeManagement.Models.ViewModels;

namespace ResumeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private ResumeDbContext db = new ResumeDbContext();

        public ActionResult Index()
        {
            var employees = db.Employees.Include(c => c.QualificationEntries.Select(b => b.Qualification)).OrderByDescending(x => x.EmployeeId).ToList();
            return View(employees);
            
        }
        public ActionResult AddNewQualification(int? id)
        {
            ViewBag.qualifications = new SelectList(db.Qualifications.ToList(), "QualificationId", "QualificationName", (id != null) ? id.ToString() : "");
            return PartialView("_AddNewQualification");
        }
       
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel vObj, int[] qualificationId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    EmployeeName = vObj.EmployeeName,
                    BirthDate = vObj.BirthDate,
                    Salary = vObj.Salary,
                    IsActive = vObj.IsActive
                };
                //for Image
                HttpPostedFileBase file = vObj.PicturePath;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    employee.Picture = filePath;
                }
                //Qualification entry
                foreach (var item in qualificationId)
                {
                    QualificationEntry qe = new QualificationEntry()
                    {
                        Employee = employee,
                        EmployeeId = employee.EmployeeId,
                        QualificationId = item,
                    };
                    db.QualificationEntries.Add(qe);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            Employee employee = db.Employees.First(x => x.EmployeeId == id);
            var qualifications = db.QualificationEntries.Where(x => x.EmployeeId == id).ToList();
           

            EmployeeViewModel vObj = new EmployeeViewModel()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Salary = employee.Salary,
                BirthDate = employee.BirthDate,
                Picture = employee.Picture,
                
                IsActive = employee.IsActive
            };
            if (qualifications.Count() > 0)
            {
                foreach (var item in qualifications)
                {
                    vObj.QualificationList.Add(item.QualificationId);
                }
            }
            return View(vObj);
        }
        
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel vObj, int[] qualificationId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeId = vObj.EmployeeId,
                    EmployeeName = vObj.EmployeeName,
                    BirthDate = vObj.BirthDate,
                    Salary = vObj.Salary,
                    IsActive = vObj.IsActive
                };
                //for Image
                HttpPostedFileBase file = vObj.PicturePath;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    employee.Picture = filePath;
                }
                else
                {
                    employee.Picture = vObj.Picture;
                }
                //spot
                var existsQualificationEntry = db.QualificationEntries.Where(x => x.EmployeeId == employee.EmployeeId).ToList();
                //Delete
                foreach (var qEntry in existsQualificationEntry)
                {
                    db.QualificationEntries.Remove(qEntry);
                }
                
                foreach (var item in qualificationId)
                {
                    QualificationEntry qEntry = new QualificationEntry()
                    {
                        EmployeeId = employee.EmployeeId,
                        QualificationId = item
                    };
                    db.QualificationEntries.Add(qEntry);
                }
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            var employee = db.Employees.Find(id);
            var existingQualifications = db.QualificationEntries.Where(x => x.EmployeeId == id).ToList();

            foreach (var qEntry in existingQualifications)
            {
                db.QualificationEntries.Remove(qEntry);
            }
            db.Entry(employee).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
