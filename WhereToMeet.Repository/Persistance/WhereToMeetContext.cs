using Microsoft.EntityFrameworkCore;
using WhereToMeet.Repository.Models;

namespace WhereToMeet.Repository.Persistance
{
    public class WhereToMeetContext : DbContext
    {
        public WhereToMeetContext(DbContextOptions<WhereToMeetContext> options) : base(options) { }

        public DbSet<Meeting> Meetings { get; set; }
    }
}
