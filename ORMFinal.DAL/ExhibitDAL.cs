using Microsoft.EntityFrameworkCore;
using ORMFinal.Models;
using System.Collections.Generic;
using System.Linq;

namespace ORMFinal.DAL
{
    public class ExhibitDAL
    {
        private readonly ORMFinalContext _context;

        public ExhibitDAL(ORMFinalContext context)
        {
            _context = context;
        }

        // Fetch all exhibits
        public List<Exhibit> GetExhibits()
        {
            return _context.Exhibits.Include(e => e.Animal).ToList(); // Include related data if necessary
        }

        // Fetch a single exhibit by Id
        public Exhibit GetExhibitById(int id)
        {
            return _context.Exhibits
                .Include(e => e.Animal)
                .FirstOrDefault(e => e.ExhibitId == id);
        }

        public void AddExhibit(Exhibit exhibit)
        {
            _context.Exhibits.Add(exhibit);
            _context.SaveChanges();
        }

        public void UpdateExhibit(Exhibit exhibit)
        {
            var existingExhibit = _context.Exhibits.Find(exhibit.ExhibitId);
            if (existingExhibit == null)
            {
                throw new ArgumentException($"Exhibit with id {exhibit.ExhibitId} not found.");
            }

            _context.Entry(existingExhibit).CurrentValues.SetValues(exhibit);
            _context.SaveChanges();
        }

        public void DeleteExhibit(int id)
        {
            var exhibit = _context.Exhibits.Find(id);
            if (exhibit != null)
            {
                _context.Exhibits.Remove(exhibit);
                _context.SaveChanges();
            }
        }

        public List<Animal> GetAllAnimals() // 新增的方法
        {
            return _context.Animals.ToList();
        }
    }
}