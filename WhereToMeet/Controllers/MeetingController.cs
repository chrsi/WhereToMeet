using System;
using Microsoft.AspNetCore.Mvc;
using WhereToMeet.BusinessLogic;
using WhereToMeet.Models;

namespace WhereToMeet.Controllers
{
    public class MeetingController : Controller
    {
        private readonly MeetingOrganizer meetingOrganizer = new MeetingOrganizer();

        [ActionName("Index")]
        public IActionResult RequestNewMeeting()
        {
            return View("CreateMeeting");
        }

        public IActionResult ShowMeeting(Guid meetingId)
        {
            Meeting meeting = meetingOrganizer.GetMeeting(meetingId);
            return View("Meeting", meeting);
        }

        [HttpPost]
        public IActionResult CreateMeeting(MeetingRequest meetingRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Meeting meeting = meetingOrganizer.CreateMeeting(meetingRequest);
            return View("Meeting", meeting);
        }
    }
}