using Microsoft.EntityFrameworkCore;

namespace Rescuer.Entites.Npgsql.Context
{
    public class NpgsqlContext : DbContext
    {
        public NpgsqlContext(DbContextOptions<NpgsqlContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<SchoolSection> SchoolSection { get; set; }
        public DbSet<SchoolSectionUser> SchoolSectionUser { get; set; }
        public DbSet<UserFile> UserFile { get; set; }
        public DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(e => e.UserFiles)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(e => e.SchoolSectionUsers)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Section>()
                .HasMany(e => e.SchoolSections)
                .WithOne(c => c.Section)
                .HasForeignKey(c => c.SectionID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<School>()
                .HasMany(e => e.SchoolSections)
                .WithOne(w => w.School)
                .HasForeignKey(f => f.SchoolID)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
