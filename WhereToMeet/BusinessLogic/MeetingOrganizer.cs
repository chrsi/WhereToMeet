using System;
using WhereToMeet.Models;

namespace WhereToMeet.BusinessLogic
{
    public class MeetingOrganizer
    {
        public Meeting CreateMeeting(MeetingRequest meetingRequest)
        {
            return new Meeting
            {
                MeetingId = Guid.NewGuid(),
                Name = meetingRequest.Name
            };
        }

        public Meeting GetMeeting(Guid meetingId)
        {
            return new Meeting
            {
                MeetingId = meetingId,
                Name = "DummyMeeting"
            };
        }
    }
}
