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
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(EmployeeService employeeService, ExhibitService exhibitService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _exhibitService = exhibitService;
            _logger = logger;
        }


        public IActionResult Index()
        {
            var employees = _employeeService.GetEmployees();
            return View(employees);
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }





        public IActionResult Create()
        {
            ViewBag.Exhibits = new SelectList(_exhibitService.GetExhibits(), "ExhibitId", "Location");
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee newEmployee)
        {
            _logger.LogInformation("Create action called.");

            // Log the state of the employee object
            _logger.LogInformation($"Received Employee: Position={newEmployee.Position}, DateStarted={newEmployee.DateStarted}, DateEnded={newEmployee.DateEnded}, ExhibitId={newEmployee.ExhibitId}");

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid.");

                try
                {
                    // Set ExhibitId to 1 for testing purposes
                    newEmployee.ExhibitId = 1;

                    _employeeService.AddEmployee(newEmployee);
                    _logger.LogInformation("Employee added successfully.");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding employee.");
                    ModelState.AddModelError("", "An error occurred while creating the employee. Please try again.");
                }
            }
            else
            {
                // Log any model state errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Model state error: {error.ErrorMessage}");
                }
            }

            // Repopulate the ViewBag for the dropdown (if you need it for other purposes)
            ViewBag.Exhibits = new SelectList(_exhibitService.GetExhibits(), "ExhibitId", "Location");
            return View(newEmployee);
        }






    }
}
