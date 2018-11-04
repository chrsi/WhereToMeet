using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WhereToMeet.Repository.Models;

namespace WhereToMeet.Repository.Persistance
{
    public class MeetingRepository
    {
        private readonly WhereToMeetContext dataContext;

        public MeetingRepository(WhereToMeetContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Meeting CreateMeeting(string meetingName)
        {
            var meeting = new Meeting
            {
                MeetingId = Guid.NewGuid(),
                Name = meetingName,
                UserLocations = new List<UserLocation>()
            };

            EntityEntry<Meeting> persistedMeeting = dataContext.Add(meeting);
            dataContext.SaveChanges();
            return persistedMeeting.Entity;
        }

        public Meeting GetMeeting(Guid meetingId)
        {
            return dataContext.Meetings.FirstOrDefault(meeting => meeting.MeetingId == meetingId);
        }
    }
}
