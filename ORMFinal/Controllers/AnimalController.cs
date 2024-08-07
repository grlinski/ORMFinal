using Microsoft.AspNetCore.Mvc;
using ORMFinal.BLL;
using ORMFinal.Models;
using System.Collections.Generic;
using System.Linq;

namespace ORMFinal.Controllers
{
    public class AnimalController : Controller
    {
        private readonly AnimalService _animalService;

        public AnimalController(AnimalService animalService)
        {
            _animalService = animalService;
        }

        public IActionResult Index(string searchString)
        {
            IEnumerable<Animal> animals = _animalService.GetAllAnimalsEnum();

            if (!string.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(a => a.AnimalName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                              a.AnimalCategory.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                              a.Habitat.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                              a.Species.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                              a.Genus.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var animalViewModels = animals.Select(a => new AnimalViewModel
            {
                AnimalId = a.AnimalId,
                AnimalCategory = a.AnimalCategory,
                Habitat = a.Habitat,
                AnimalName = a.AnimalName,
                Species = a.Species,
                Genus = a.Genus,
                MorningFeeding = a.FeedingSchedule?.MorningFeeding,
                NoonFeeding = a.FeedingSchedule?.NoonFeeding,
                EveningFeeding = a.FeedingSchedule?.EveningFeeding,
                NightFeeding = a.FeedingSchedule?.NightFeeding,
                HealthStatus = a.AnimalHealth?.HealthStatus,
                ReportDate = a.AnimalHealth?.ReportDate,
                LastVaccinationDate = a.AnimalHealth?.LastVaccinationDate
            }).ToList();
            return View(animalViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnimalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newAnimal = new Animal
                {
                    AnimalCategory = viewModel.AnimalCategory,
                    Habitat = viewModel.Habitat,
                    AnimalName = viewModel.AnimalName,
                    Species = viewModel.Species,
                    Genus = viewModel.Genus,
                    FeedingSchedule = new FeedingSchedule
                    {
                        MorningFeeding = viewModel.MorningFeeding,
                        NoonFeeding = viewModel.NoonFeeding,
                        EveningFeeding = viewModel.EveningFeeding,
                        NightFeeding = viewModel.NightFeeding
                    },
                    AnimalHealth = new AnimalHealth
                    {
                        HealthStatus = viewModel.HealthStatus,
                        ReportDate = viewModel.ReportDate ?? DateTime.Now,
                        LastVaccinationDate = viewModel.LastVaccinationDate ?? DateTime.Now
                    }
                };

                _animalService.AddAnimal(newAnimal);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return NotFound("The id received was null");
            }

            var animal = _animalService.GetAnimalById(id);

            if (animal == null)
            {
                return NotFound("There is no animal with that id");
            }

            var animalViewModel = new AnimalViewModel
            {
                AnimalId = animal.AnimalId,
                AnimalCategory = animal.AnimalCategory,
                Habitat = animal.Habitat,
                AnimalName = animal.AnimalName,
                Species = animal.Species,
                Genus = animal.Genus
            };

            return View(animalViewModel);
        }

        [HttpPost]
        public IActionResult Update(AnimalViewModel animalViewModel)
        {
            if (ModelState.IsValid)
            {
                var animal = _animalService.GetAnimalById(animalViewModel.AnimalId);
                if (animal == null)
                {
                    return NotFound("There is no animal with that id");
                }

                animal.AnimalCategory = animalViewModel.AnimalCategory;
                animal.Habitat = animalViewModel.Habitat;
                animal.AnimalName = animalViewModel.AnimalName;
                animal.Species = animalViewModel.Species;
                animal.Genus = animalViewModel.Genus;

                _animalService.UpdateAnimal(animal);
                return RedirectToAction("Index");
            }
            return View(animalViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _animalService.DeleteAnimal(id);
            return RedirectToAction("Index");
        }
    }
}
