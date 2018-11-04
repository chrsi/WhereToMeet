using System;
using Microsoft.AspNetCore.Mvc;
using WhereToMeet.Models;

namespace WhereToMeet.Controllers
{
    public class MeetingController : Controller
    {
        public IActionResult Index(Guid meetingId)
        {
            var meeting = new Meeting { MeetingId = meetingId };
            return View(meeting);
        }
    }
}