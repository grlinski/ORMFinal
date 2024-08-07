﻿using Microsoft.Extensions.Logging;
using ORMFinal.DAL;
using ORMFinal.Models;
using System.Collections.Generic;

namespace ORMFinal.BLL
{
    public class AnimalService
    {
        private readonly AnimalDAL _animalDAL;
        private readonly ILogger<AnimalService> _logger;

        public AnimalService(AnimalDAL animalDAL, ILogger<AnimalService> logger)
        {
            _animalDAL = animalDAL;
            _logger = logger;
        }

        public List<Animal> GetAllAnimalsList()
        {
            var animals = _animalDAL.GetAllAnimals().ToList();
            if (animals == null || !animals.Any())
            {
                _logger.LogWarning("No animals found");
                return new List<Animal>();
            }
            return animals;
        }

        public IEnumerable<Animal> GetAllAnimalsEnum()
        {
            return _animalDAL.GetAllAnimals();
        }

        public void AddAnimal(Animal animal)
        {
            _animalDAL.AddAnimal(animal);
        }

        public Animal GetAnimalById(int id)
        {
            return _animalDAL.GetAnimalById(id);
        }

        public void UpdateAnimal(Animal animal)
        {
            _animalDAL.UpdateAnimal(animal);
        }

        public void DeleteAnimal(int id)
        {
            _animalDAL.DeleteAnimal(id);
        }
    }
}
