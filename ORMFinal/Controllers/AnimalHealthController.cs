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

            //Updated to also include the information from animals
            var animalHealthList = _animalHealthService.GetAnimalHealthPlusAnimals();
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
                //This will create some default values if they are not set
                animalHealth.ReportDate = animalHealth.ReportDate == default ? DateTime.Now : animalHealth.ReportDate;
                animalHealth.LastVaccinationDate = animalHealth.LastVaccinationDate == default ? DateTime.Now : animalHealth.LastVaccinationDate;

                // Add animal health record
                _animalHealthService.AddAnimalHealth(animalHealth);
                return RedirectToAction("Index");
            }

            //Not sure the rest is needed
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


        // Delete method
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _animalHealthService.DeleteAnimalHealth(id);
            return RedirectToAction("Index");
        }

    }
}