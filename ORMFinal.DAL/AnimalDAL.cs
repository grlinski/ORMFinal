using Microsoft.EntityFrameworkCore;
using ORMFinal.Models;
using System.Collections.Generic;
using System.Linq;

namespace ORMFinal.DAL
{
    public class AnimalDAL
    {
        private readonly ORMFinalContext _context;

        public AnimalDAL(ORMFinalContext context)
        {
            _context = context;
        }

        public void AddAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return _context.Animals
                .Include(a => a.FeedingSchedule)
                .Include(a => a.AnimalHealth)
                .Include(a => a.Exhibits)
                .ToList();
        }

        public Animal GetAnimalById(int id)
        {
            return _context.Animals
                .Include(a => a.FeedingSchedule)
                .Include(a => a.AnimalHealth)
                .Include(a => a.Exhibits)
                .FirstOrDefault(a => a.AnimalId == id);
        }

        public void UpdateAnimal(Animal animal)
        {
            _context.Animals.Update(animal);
            _context.SaveChanges();
        }


        //This needs to delete a slew of other values
        public void DeleteAnimal(int id)
        {
            var animal = _context.Animals
                .Include(a => a.FeedingSchedule)
                .Include(a => a.AnimalHealth)
                .Include(a => a.Exhibits)
                .FirstOrDefault(a => a.AnimalId == id);

            if (animal != null)
            {
                _context.Animals.Remove(animal);
                _context.SaveChanges();
            }
        }




    }
}
