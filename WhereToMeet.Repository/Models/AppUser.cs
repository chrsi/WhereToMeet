using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhereToMeet.Repository.Models
{
    public class AppUser: IdentityUser
    {
        public string ProfileImage { get; set; }
    }
}
