using System;
using System.Collections.Generic;

namespace WhereToMeet.Repository.Models
{

    public class Meeting
    {
        public Guid Id { get; set; }
        public Guid MeetingId { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserLocation> UserLocations { get; set; }
    }
}
