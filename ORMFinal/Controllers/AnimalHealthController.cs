using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ORMFinal.BLL;
using ORMFinal.DAL;
using ORMFinal.Models;

namespace ORMFinal.Controllers
{
    public class AnimalHealthController : Controller
    {
        private readonly AnimalHealthService _animalHealthService;
        private readonly AnimalHealthDAL _animalHealthDAL;
        private readonly AnimalService _animalService;

        public AnimalHealthController(AnimalHealthService animalHealthService, AnimalHealthDAL animalHealthDAL, AnimalService animalService)
        {
            _animalHealthService = animalHealthService;
            _animalHealthDAL = animalHealthDAL;
            _animalService = animalService;
        }



        [HttpGet]
        public IActionResult Index()
        {
            var animalHealthList = _animalHealthService.GetAnimalHealthPlusAnimals();

            //var animalHealthList = _animalHealthService.GetAnimalHealth();
            return View(animalHealthList);
        }



        //Create Functions
        [HttpGet]
        public IActionResult Create()
        {
            // Populate ViewBag with animals to select from
            ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName");
            return View(new AnimalHealth());
        }


        [HttpPost]
        public IActionResult Create(AnimalHealth animalHealth, string action)
        {

            if (action == "CreateAnimalHealth")
            {
                // Assign default values if necessary
                animalHealth.ReportDate = animalHealth.ReportDate == default ? DateTime.Now : animalHealth.ReportDate;
                animalHealth.LastVaccinationDate = animalHealth.LastVaccinationDate == default ? DateTime.Now : animalHealth.LastVaccinationDate;

                // Add animal health record
                _animalHealthService.AddAnimalHealth(animalHealth);
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                if (animalHealth.AnimalId == 0)
                {
                    ModelState.AddModelError("AnimalId", "Please select an animal.");
                    ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName");
                    return View(animalHealth);
                }

                _animalHealthService.AddAnimalHealth(animalHealth);
                return RedirectToAction("Index");
            }

            ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName");
            return View(animalHealth);
        }

    }
}