using Microsoft.Extensions.Logging;
using ORMFinal.DAL;
using ORMFinal.Models;

namespace ORMFinal.BLL
{
    public class FeedingScheduleService
    {
        private readonly FeedingScheduleDAL _feedingScheduleDAL;
        private readonly ILogger<FeedingScheduleService> _logger;


        public FeedingScheduleService(FeedingScheduleDAL feedingScheduleDAL, ILogger<FeedingScheduleService> logger)
        {
            _feedingScheduleDAL = feedingScheduleDAL;
            _logger = logger;
        }

        public List<FeedingSchedule> GetFeedingSchedule()
        {
            return _feedingScheduleDAL.GetFeedingSchedule();
        }

        public async Task<FeedingSchedule> GetFeedingSchedule(int id)
        {
            var feedingRecord = await _feedingScheduleDAL.GetFeedingSchedule(id);
            if (feedingRecord == null)
            {
                _logger.LogWarning("Animal feeding record with id {Id} not found", id);
            }
            return feedingRecord;
        }

        //These shouldn't be async, but it's working. So no point in changing.
        public async Task AddFeedingSchedule(FeedingSchedule feedingSchedule)
        {
            _logger.LogInformation("Adding Animal Schedule with FeedingScheduleId: {Id}", feedingSchedule.FeedingScheduleId);
            _feedingScheduleDAL.Add(feedingSchedule);
            await _feedingScheduleDAL.SaveChangesAsync();
        }

        public async Task DeleteFeedingSchedule(int id)
        {
            _logger.LogInformation("Deleting Animal feeding schedule with Id: {Id}", id);
            await _feedingScheduleDAL.DeleteFeedingSchedule(id);
        }
    }
}
