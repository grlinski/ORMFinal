using Microsoft.Extensions.Logging;
using ORMFinal.DAL;
using ORMFinal.Models;

namespace ORMFinal.BLL
{
    public class ExhibitService
    {
        private readonly ExhibitDAL _exhibitDAL;
        private readonly AnimalService _animalService;
        private readonly ILogger<ExhibitService> _logger;

        public ExhibitService(ExhibitDAL exhibitDAL, AnimalService animalService, ILogger<ExhibitService> logger)
        {
            _exhibitDAL = exhibitDAL;
            _animalService = animalService;
            _logger = logger;
        }

        public List<Exhibit> GetExhibits()
        {
            _logger.LogInformation("Fetching all exhibits.");
            return _exhibitDAL.GetExhibits();
        }

        public Exhibit GetExhibitById(int id)
        {
            var exhibit = _exhibitDAL.GetExhibitById(id);
            if (exhibit == null)
            {
                _logger.LogWarning("Exhibit with id {Id} not found", id);
            }
            return exhibit;
        }

        public void AddExhibit(Exhibit exhibit)
        {
            _logger.LogInformation("Adding Exhibit with ExhibitId: {Id}", exhibit.ExhibitId);
            _exhibitDAL.AddExhibit(exhibit);
        }

        public void UpdateExhibit(Exhibit exhibit)
        {
            _logger.LogInformation("Updating Exhibit with ExhibitId: {Id}, AnimalId: {AnimalId}", exhibit.ExhibitId, exhibit.AnimalId);
            _exhibitDAL.UpdateExhibit(exhibit);
        }

        public void DeleteExhibit(int id)
        {
            _logger.LogInformation("Deleting Exhibit with Id: {Id}", id);
            _exhibitDAL.DeleteExhibit(id);
        }

        public List<Animal> GetAllAnimals()
        {
            return _animalService.GetAllAnimals();
        }

        public Animal GetAnimalById(int animalId)
        {
            return _animalService.GetAllAnimals().FirstOrDefault(a => a.AnimalId == animalId);
        }
    }
}
