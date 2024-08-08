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
            return _context.Animals.ToList();
        }

        public Animal GetAnimalById(int id)
        {
            return _context.Animals.Find(id);
        }

        public void UpdateAnimal(Animal animal)
        {
            _context.Animals.Update(animal);
            _context.SaveChanges();
        }

        public void DeleteAnimal(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                _context.SaveChanges();
            }
        }




    }
}
