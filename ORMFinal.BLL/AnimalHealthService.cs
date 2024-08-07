using Microsoft.Extensions.Logging;
using ORMFinal.DAL;
using ORMFinal.Models;

namespace ORMFinal.BLL
{
    public class AnimalHealthService
    {
        private readonly AnimalHealthDAL _animalHealthDAL;
        private readonly ILogger<AnimalHealthService> _logger;


        public AnimalHealthService(AnimalHealthDAL animalHealthDAL, ILogger<AnimalHealthService> logger)
        {
            _animalHealthDAL = animalHealthDAL;
            _logger = logger;
        }

        public List<AnimalHealth> GetAnimalHealth()
        {
            return _animalHealthDAL.GetAnimalHealth();
        }

        public async Task<AnimalHealth> GetAnimalHealth(int id)
        {
            var healthRecord = await _animalHealthDAL.GetAnimalHealth(id);
            if (healthRecord == null)
            {
                _logger.LogWarning("Animal health record with id {Id} not found", id);
            }
            return healthRecord;
        }

        public async Task AddAnimalHealth(AnimalHealth animalHealth)
        {
            _logger.LogInformation("Adding Animal Health Report with HealthReportId: {Id}", animalHealth.HealthReportId);
            _animalHealthDAL.Add(animalHealth);
            await _animalHealthDAL.SaveChangesAsync();
        }

        public async Task UpdateAnimalHealth(AnimalHealth animalHealth)
        {
            _logger.LogInformation("Updating Animal Health Report with HealthReportId: {Id}", animalHealth.HealthReportId);
            _animalHealthDAL.UpdateAnimalHealth(animalHealth);
            await _animalHealthDAL.SaveChangesAsync();
        }

        public async Task DeleteAnimalHealth(int id)
        {
            _logger.LogInformation("Deleting Animal Health Report with Id: {Id}", id);
            await _animalHealthDAL.DeleteAnimalHealth(id);
        }
        public List<AnimalHealth> GetAnimalHealthPlusAnimals()
        {
            return _animalHealthDAL.GetAnimalHealthPlusAnimals();
        }

    }
}
