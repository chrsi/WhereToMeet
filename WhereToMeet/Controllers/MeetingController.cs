using System;
using Microsoft.AspNetCore.Mvc;
using WhereToMeet.BusinessLogic;
using WhereToMeet.Models;

namespace WhereToMeet.Controllers
{
    public class MeetingController : Controller
    {
        private readonly MeetingOrganizer meetingOrganizer;

        public MeetingController(MeetingOrganizer meetingOrganizer)
        {
            this.meetingOrganizer = meetingOrganizer;
        }

        [ActionName("Index")]
        public IActionResult RequestNewMeeting()
        {
            return View("CreateMeeting");
        }

        public IActionResult ShowMeeting(Guid meetingId)
        {
            Meeting meeting = meetingOrganizer.GetMeeting(meetingId);
            if (meeting == null) return BadRequest();

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
            if (meeting == null) return StatusCode(500);

            return RedirectToAction("ShowMeeting", new { meetingId = meeting.MeetingId });
        }
    }
}