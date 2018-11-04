using System;

namespace WhereToMeet.Repository.Models
{
    public class UserLocation
    {
        public Guid Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string SessionId { get; set; }
    }
}