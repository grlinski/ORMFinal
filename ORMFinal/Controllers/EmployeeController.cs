
using Microsoft.AspNetCore.Mvc;
using ORMFinal.BLL;
using ORMFinal.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ORMFinal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly ExhibitService _exhibitService;

        public EmployeeController(EmployeeService employeeService, ExhibitService exhibitService)
        {
            _employeeService = employeeService;
            _exhibitService = exhibitService;
        }

        // Index method with search and sort functionality
        [HttpGet]
        public IActionResult Index(string searchPosition, string searchExhibit, string sortOrder)
        {
            var employees = _employeeService.GetEmployees();

            // Filter by search criteria
            if (!string.IsNullOrEmpty(searchPosition))
            {
                employees = employees.Where(e => e.Position.Contains(searchPosition)).ToList();
            }

            if (!string.IsNullOrEmpty(searchExhibit))
            {
                employees = employees.Where(e => e.Exhibit != null && e.Exhibit.Location.Contains(searchExhibit)).ToList();
            }

            return View(employees);
        }

        // Create methods
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Exhibits = new SelectList(_exhibitService.GetExhibits(), "ExhibitId", "Location");
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            ViewBag.Exhibits = new SelectList(_exhibitService.GetExhibits(), "ExhibitId", "Location");
            return View(employee);
        }

        // Delete method
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        // Edit methods
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Exhibits = new SelectList(_exhibitService.GetExhibits(), "ExhibitId", "Location", employee.ExhibitId);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            ViewBag.Exhibits = new SelectList(_exhibitService.GetExhibits(), "ExhibitId", "Location", employee.ExhibitId);
            return View(employee);
        }
    }
}

