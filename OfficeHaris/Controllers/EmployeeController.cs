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
            //ViewData["employees"] = oe.Employees.ToList();

            //return View();

            List<Employee> employees = oe.Employees.ToList();
            return View(employees);
        }

        public ActionResult CreateEmployee()
        {
            OfficeEntities oe = new OfficeEntities();
            ViewBag.departments = oe.Departments.ToList();

            Employee employee = new Employee();
            return View(employee);
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            //Employee employee = new Employee();
            //employee.Name = formCollection["name"];
            //employee.Age = int.Parse(string.IsNullOrEmpty(formCollection["age"]) ? "0" : formCollection["age"]);
            //employee.Gender = formCollection["gender"];
            //employee.DepartmentId = int.Parse(string.IsNullOrEmpty(formCollection["departmentId"]) ? "0" : formCollection["departmentId"]);
            //employee.Designation = formCollection["designation"];
            //employee.Address = formCollection["address"];

            try
            {
                OfficeEntities oe = new OfficeEntities();
                //oe.Employees.Add(employee);
                oe.Entry(employee).State = System.Data.Entity.EntityState.Added;
                oe.SaveChanges();

                TempData["success"] = "Record inserted.";
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                TempData["success"] = "Something went wrong.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditEmployee(int employee_id = 0)
        {
            Employee employee = new Employee();

            if (employee_id != 0)
            {
                OfficeEntities oe = new OfficeEntities();
                ViewBag.departments = oe.Departments.ToList();

                if (oe.Employees.ToList().Exists(e => e.Id == employee_id))
                    employee = oe.Employees.Where(e => e.Id == employee_id).Single();
            }

            Response.StatusCode = 404;
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee employee)
        {
            //Employee employee = new Employee();
            //employee.Name = formCollection["name"];
            //employee.Age = int.Parse(string.IsNullOrEmpty(formCollection["age"]) ? "0" : formCollection["age"]);
            //employee.Gender = formCollection["gender"];
            //employee.DepartmentId = int.Parse(string.IsNullOrEmpty(formCollection["departmentId"]) ? "0" : formCollection["departmentId"]);
            //employee.Designation = formCollection["designation"];
            //employee.Address = formCollection["address"];

            OfficeEntities oe = new OfficeEntities();
            oe.Entry(employee).State = System.Data.Entity.EntityState.Modified;

            oe.SaveChanges();

            TempData["success"] = "Record updated.";

            return RedirectToAction("Index");
        }
    }
}