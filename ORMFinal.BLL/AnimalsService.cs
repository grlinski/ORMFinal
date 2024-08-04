using Microsoft.Extensions.Logging;
using ORMFinal.DAL;
using ORMFinal.Models;

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

        public List<Animal> GetAllAnimals()
        {
            var animals = _animalDAL.GetAllAnimals();
            if (animals == null || !animals.Any())
            {
                _logger.LogWarning("No animals found");
                return new List<Animal>();
            }
            return animals;
        }
    }
}
