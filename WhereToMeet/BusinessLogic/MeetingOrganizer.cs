using System;
using WhereToMeet.Models;
using WhereToMeet.Repository.Persistance;
using RepositoryModels = WhereToMeet.Repository.Models;

namespace WhereToMeet.BusinessLogic
{
    public class MeetingOrganizer
    {
        private readonly MeetingRepository meetingRepository;

        public MeetingOrganizer(MeetingRepository meetingRepository)
        {
            this.meetingRepository = meetingRepository;
        }

        public Meeting CreateMeeting(MeetingRequest meetingRequest)
        {
            RepositoryModels.Meeting persistedMeeting = meetingRepository.CreateMeeting(meetingRequest.Name);
            return new Meeting
            {
                Name = persistedMeeting.Name,
                MeetingId = persistedMeeting.MeetingId
            };
        }

        public Meeting GetMeeting(Guid meetingId)
        {
            RepositoryModels.Meeting persistedMeeting = meetingRepository.GetMeeting(meetingId);
            return new Meeting
            {
                Name = persistedMeeting.Name,
                MeetingId = persistedMeeting.MeetingId
            };
        }
    }
}
