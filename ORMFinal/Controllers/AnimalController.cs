﻿using Microsoft.AspNetCore.Mvc;
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



        //Gabe's comments so I actually vaguely know what is going on.
        //This takes in a string, ignores case sensitivity and searches categories/properties for it
        public IActionResult Index(string searchString)
        {
            //Returns animals as an enmuerable. There is a also a similar list version of the function: GetAllAnimalsList
            IEnumerable<Animal> animals = _animalService.GetAllAnimalsEnum();

            if (!string.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(a => a.AnimalName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                              a.AnimalCategory.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                              a.Habitat.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                              a.Species.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                              a.Genus.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            //The animal object
            var animalViewModels = animals.Select(a => new Animal
            {
                AnimalId = a.AnimalId,
                AnimalCategory = a.AnimalCategory,
                Habitat = a.Habitat,
                AnimalName = a.AnimalName,
                Species = a.Species,
                Genus = a.Genus,

            }).ToList();
            return View(animalViewModels);
        }



        //Create Functions
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
                };

                _animalService.AddAnimal(newAnimal);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        //Update/Edit Functions

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return NotFound("The id received was null");
            }

            //Locate the Animal based on id
            var animal = _animalService.GetAnimalById(id);

            if (animal == null)
            {
                return NotFound("There is no animal with that id found in your database");
            }

            var animalViewModel = new Animal
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
                    return NotFound("There is no animal with that id found in your database");
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
            try
            {
                _animalService.DeleteAnimal(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

    }
}
