using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Persistence.Context;

public class ApplicationDbContext:  IdentityDbContext<AppUser, IdentityRole, string>
{
    
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
}