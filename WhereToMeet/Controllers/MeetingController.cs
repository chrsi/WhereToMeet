﻿using System;
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
        public IActionResult Create()
        {
            return View("Create");
        }

        public IActionResult Details(Guid meetingId)
        {
            Meeting meeting = meetingOrganizer.GetMeeting(meetingId);
            if (meeting == null) return BadRequest();

            return View("Details", meeting);
        }

        [HttpPost]
        public IActionResult Create(MeetingRequest meetingRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Meeting meeting = meetingOrganizer.CreateMeeting(meetingRequest);
            if (meeting == null) return StatusCode(500);

            return RedirectToAction("Details", new { meetingId = meeting.MeetingId });
        }
    }
}