using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhereToMeet.Repository.Models;

namespace WhereToMeet.Repository.Persistance
{
    public class WhereToMeetContext : IdentityDbContext<AppUser>
    {
        public WhereToMeetContext(DbContextOptions<WhereToMeetContext> options) : base(options) { }

        public DbSet<Meeting> Meetings { get; set; }
    }
}
