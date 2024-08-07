using Microsoft.AspNetCore.Mvc;
using ORMFinal.BLL;
using ORMFinal.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ORMFinal.Controllers
{
    public class ExhibitController : Controller
    {
        private readonly ExhibitService _exhibitService;
        private readonly EmployeeService _employeeService;
        private readonly AnimalService _animalService;
        private readonly ILogger<ExhibitController> _logger;

        public ExhibitController(ExhibitService exhibitService, EmployeeService employeeService, AnimalService animalService, ILogger<ExhibitController> logger)
        {
            _exhibitService = exhibitService;
            _employeeService = employeeService;
            _animalService = animalService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var exhibits = _exhibitService.GetExhibits();
            return View(exhibits);
        }


        //Index Search
        [HttpGet]
        public IActionResult Index(string searchLocation, string searchSize)
        {
            var exhibits = _exhibitService.GetExhibits();

            if (!string.IsNullOrEmpty(searchLocation))
            {
                exhibits = exhibits.Where(e => e.Location.Contains(searchLocation)).ToList();
            }

            if (!string.IsNullOrEmpty(searchSize))
            {
                exhibits = exhibits.Where(e => e.Size.Contains(searchSize)).ToList();
            }

            ViewBag.SearchLocation = searchLocation;
            ViewBag.SearchSize = searchSize;

            return View(exhibits);
        }




        // Delete Exhibit
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var exhibit = _exhibitService.GetExhibitById(id);
                if (exhibit == null)
                {
                    return NotFound();
                }

                _exhibitService.DeleteExhibit(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Cannot delete this exhibit as it is associated with employees.";
                return RedirectToAction(nameof(Index));
            }
        }

        // Creation Functions
        [HttpGet]
        public IActionResult Create()
        {
            //Delete Employees later, not sure it's doing anything
            ViewBag.Employees = _employeeService.GetEmployees();
            ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName");
            return View(new Exhibit());
        }

        [HttpPost]
        public IActionResult Create(Exhibit exhibit, string action)
        {
            _logger.LogInformation("Create POST action called with action: {Action}", action);

            if (action == "CreateExhibit")
            {
                var random = new Random();
                exhibit.Location = string.IsNullOrEmpty(exhibit.Location) ? $"Random Location {random.Next(1, 100)}" : exhibit.Location;
                exhibit.Size = string.IsNullOrEmpty(exhibit.Size) ? $"{random.Next(100, 500)} sq ft" : exhibit.Size;

                _exhibitService.AddExhibit(exhibit);
                _logger.LogInformation("Created Exhibit with Id: {Id}", exhibit.ExhibitId);
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                if (exhibit.AnimalId == 0)
                {
                    ModelState.AddModelError("AnimalId", "Please select an animal.");
                    ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName");
                    return View(exhibit);
                }

                _exhibitService.AddExhibit(exhibit);
                _logger.LogInformation("Created Exhibit with Id: {Id}", exhibit.ExhibitId);
                return RedirectToAction("Index");
            }

            _logger.LogWarning("Create POST action failed. Model state is invalid.");
            ViewBag.Employees = _employeeService.GetEmployees();
            return View(exhibit);
        }

        // Edit Functions
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var exhibit = _exhibitService.GetExhibitById(id);
            if (exhibit == null)
            {
                _logger.LogWarning("Exhibit with ExhibitId: {ExhibitId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Fetched exhibit: {@Exhibit}", exhibit);

            // Populate the animals dropdown
            ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName", exhibit.AnimalId);

            return View(exhibit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ExhibitId,Location,Size,AnimalId")] Exhibit exhibit)
        {
            _logger.LogInformation("Edit POST action called for Exhibit Id: {Id}", id);
            _logger.LogInformation("Received Exhibit data: ExhibitId={ExhibitId}, Location={Location}, Size={Size}, AnimalId={AnimalId}",
                exhibit.ExhibitId, exhibit.Location, exhibit.Size, exhibit.AnimalId);

            if (id != exhibit.ExhibitId)
            {
                _logger.LogWarning("ExhibitId mismatch: {Id} != {ExhibitId}", id, exhibit.ExhibitId);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _exhibitService.UpdateExhibit(exhibit);
                    _logger.LogInformation("Exhibit with ExhibitId: {ExhibitId} updated successfully", exhibit.ExhibitId);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating Exhibit with ExhibitId: {ExhibitId}", exhibit.ExhibitId);
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            else
            {
                _logger.LogWarning("Edit POST action failed. Model state is invalid for Exhibit Id: {Id}", id);

                // Log the ModelState errors
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogWarning("Validation Error - Property: {Property}, Error: {ErrorMessage}", state.Key, error.ErrorMessage);
                    }
                }
            }

            // Ensure the select list is available in case of a model state error
            ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName", exhibit.AnimalId);
            return View(exhibit);
        }

    }
}

