using Microsoft.EntityFrameworkCore;
using ORMFinal.Models;

namespace ORMFinal.DAL
{
    public class FeedingScheduleDAL
    {
        private readonly ORMFinalContext _context;

        public FeedingScheduleDAL(ORMFinalContext context)
        {
            _context = context;
        }

        public List<FeedingSchedule> GetFeedingSchedule()
        {
            return _context.FeedingSchedules.ToList();
        }

        public async Task<FeedingSchedule> GetFeedingSchedule(int id)
        {
            return await _context.FeedingSchedules
                .Include(ah => ah.Animal)
                .FirstOrDefaultAsync(ah => ah.FeedingScheduleId == id);
        }

        public void UpdateFeedingSchedule(FeedingSchedule feedingSchedule)
        {
            var existingFeedingSchedule = _context.FeedingSchedules.Find(feedingSchedule.FeedingScheduleId);
            if (existingFeedingSchedule == null)
            {
                throw new ArgumentException($"Feeding schedule record with id {feedingSchedule.FeedingScheduleId} not found.");
            }

            _context.Entry(existingFeedingSchedule).CurrentValues.SetValues(feedingSchedule);
            _context.Entry(existingFeedingSchedule).State = EntityState.Modified;
        }

        public void Add(FeedingSchedule feedingSchedule)
        {
            _context.FeedingSchedules.Add(feedingSchedule);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public List<Animal> GetAnimalCategories()
        {
            return _context.Animals.ToList();
        }


        public async Task DeleteFeedingSchedule(int id)
        {
            var feedingSchedule = await _context.FeedingSchedules.FindAsync(id);
            if (feedingSchedule != null)
            {
                _context.FeedingSchedules.Remove(feedingSchedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
