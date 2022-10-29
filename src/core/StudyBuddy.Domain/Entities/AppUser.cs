using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyBuddy.Domain.Entities
{
    public class AppUser: IdentityUser
    {
        //public virtual List<Classroom> Classrooms { get; set; }
        public ICollection<UserClassroom> Classrooms { get; set; }
        public List<Message> Messages { get; set; }
    }
}
