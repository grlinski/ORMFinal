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

        public List<Animal> GetAllAnimals()
        {
            return _context.Animals.ToList();
        }
    }
}
