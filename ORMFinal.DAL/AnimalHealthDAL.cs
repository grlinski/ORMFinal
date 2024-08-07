using Microsoft.EntityFrameworkCore;
using ORMFinal.Models;

namespace ORMFinal.DAL
{
    public class AnimalHealthDAL
    {
        private readonly ORMFinalContext _context;

        public AnimalHealthDAL(ORMFinalContext context)
        {
            _context = context;
        }

        public List<AnimalHealth> GetAnimalHealth()
        {
            return _context.AnimalHealths.ToList();
        }

        public async Task<AnimalHealth> GetAnimalHealth(int id)
        {
            return await _context.AnimalHealths
                .Include(ah => ah.Animal)
                .FirstOrDefaultAsync(ah => ah.HealthReportId == id);
        }

        public void UpdateAnimalHealth(AnimalHealth animalHealth)
        {
            var existingHealthRecord = _context.AnimalHealths.Find(animalHealth.HealthReportId);
            if (existingHealthRecord == null)
            {
                throw new ArgumentException($"Animal health record with id {animalHealth.HealthReportId} not found.");
            }

            _context.Entry(existingHealthRecord).CurrentValues.SetValues(animalHealth);
        }



        public void Add(AnimalHealth animalHealth)
        {
            _context.AnimalHealths.Add(animalHealth);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public List<Animal> GetAnimalCategories()
        {
            return _context.Animals.ToList();
        }


        public async Task DeleteAnimalHealth(int id)
        {
            var animalHealth = await _context.AnimalHealths.FindAsync(id);
            if (animalHealth != null)
            {
                _context.AnimalHealths.Remove(animalHealth);
                await _context.SaveChangesAsync();
            }
        }

        public List<AnimalHealth> GetAnimalHealthPlusAnimals()
        {
            return _context.AnimalHealths.Include(a => a.Animal).ToList();
        }
    }
}