using System;
using Microsoft.AspNetCore.Mvc;

namespace WhereToMeet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GoToMeeting([FromForm] Guid meetingId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Meeting", new { meetingId = meetingId });
        }
    }
}