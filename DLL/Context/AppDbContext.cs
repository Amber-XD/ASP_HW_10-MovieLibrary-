    using Domain.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    namespace DLL.Context
    {
        public class AppDbContext : IdentityDbContext<User>
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
            public DbSet<Movie> Movies { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                builder.Entity<User>()
                    .HasMany(u => u.OldMovie)
                    .WithOne(m => m.User)
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

        }
    }
