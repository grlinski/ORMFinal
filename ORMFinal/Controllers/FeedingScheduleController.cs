using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ORMFinal.BLL;
using ORMFinal.DAL;
using ORMFinal.Models;

namespace ORMFinal.Controllers
{
    public class FeedingScheduleController : Controller
    {
        private readonly FeedingScheduleService _feedingScheduleService;
        private readonly FeedingScheduleDAL _feedingScheduleDAL;
        private readonly AnimalService _animalService;

        public FeedingScheduleController(FeedingScheduleService feedingScheduleService, FeedingScheduleDAL feedingScheduleDAL, AnimalService animalService)
        {
            _feedingScheduleService = feedingScheduleService;
            _feedingScheduleDAL = feedingScheduleDAL;
            _animalService = animalService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var feedingScheduleList = _feedingScheduleService.GetFeedingSchedule();
            return View(feedingScheduleList);
        }


        //Create Functions
        [HttpGet]
        public IActionResult Create()
        {
            //Add animals to the viewbag
            ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName");
            return View(new FeedingSchedule());
        }


        [HttpPost]
        public IActionResult Create(FeedingSchedule feedingSchedule, string action)
        {

            if (action == "CreateFeedingSchedule")
            {
                //Default values if none provided
                feedingSchedule.MorningFeeding = feedingSchedule.MorningFeeding == default ? DateTime.Now : feedingSchedule.MorningFeeding;
                feedingSchedule.NoonFeeding = feedingSchedule.NoonFeeding == default ? DateTime.Now : feedingSchedule.NoonFeeding;
                feedingSchedule.EveningFeeding = feedingSchedule.EveningFeeding == default ? DateTime.Now : feedingSchedule.EveningFeeding;
                feedingSchedule.NightFeeding = feedingSchedule.NightFeeding == default ? DateTime.Now : feedingSchedule.NightFeeding;

                // Add animal health record
                _feedingScheduleService.AddFeedingSchedule(feedingSchedule);
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                if (feedingSchedule.AnimalId == 0)
                {
                    ModelState.AddModelError("AnimalId", "Please select an animal.");
                    ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName");
                    return View(feedingSchedule);
                }

                _feedingScheduleService.AddFeedingSchedule(feedingSchedule);
                return RedirectToAction("Index");
            }

            ViewBag.Animals = new SelectList(_animalService.GetAllAnimalsList(), "AnimalId", "AnimalName");
            return View(feedingSchedule);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _feedingScheduleService.DeleteFeedingSchedule(id);
            return RedirectToAction("Index");
        }
    }
}
