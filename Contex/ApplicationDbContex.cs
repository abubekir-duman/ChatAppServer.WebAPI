using ChatAppServer.WebAPI.Enums;
using ChatAppServer.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Contex;

public class ApplicationDbContex : DbContext
{
    public ApplicationDbContex(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(p => p.Status)
            .HasConversion(type => type.Value, value => UserStatusEnum.FromValue(value));
    }
}
