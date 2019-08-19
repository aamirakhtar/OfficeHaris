using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeHaris.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            OfficeEntities oe = new OfficeEntities();
            ViewData["employees"] = oe.Employees.ToList();

            return View();
        }

        public ActionResult CreateEmployee()
        {
            OfficeEntities oe = new OfficeEntities();
            ViewBag.departments = oe.Departments.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(FormCollection formCollection)
        {
            Employee employee = new Employee();
            employee.Name = formCollection["name"];
            employee.Age = int.Parse(string.IsNullOrEmpty(formCollection["age"]) ? "0" : formCollection["age"]);
            employee.Gender = formCollection["gender"];
            employee.DepartmentId = int.Parse(string.IsNullOrEmpty(formCollection["departmentId"]) ? "0" : formCollection["departmentId"]);
            employee.Designation = formCollection["designation"];
            employee.Address = formCollection["address"];

            OfficeEntities oe = new OfficeEntities();
            oe.Employees.Add(employee);
            oe.SaveChanges();

            TempData["success"] = "Record inserted.";

            return RedirectToAction("Index");
        }
    }
}