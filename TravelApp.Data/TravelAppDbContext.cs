using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelApp.Data.Entities;
using TravelApp.Data.Seeds;

namespace TravelApp.Data
{
    /// <summary>
    /// This class holds TravelAppDbContext
    /// </summary>
    public class TravelAppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly bool seedDb;
        public TravelAppDbContext(DbContextOptions<TravelAppDbContext> options, bool seed = true)
            : base(options)
        {
            if (this.Database.IsRelational())
            {
                this.Database.Migrate();
            }
            else
            {
                this.Database.EnsureCreated();
                this.seedDb = seed;
            }
        }

        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Journey> Journeys { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Town> Towns { get; set; } = null!;
        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<ApplicationUserJourney> ApplicationUsersJourneys { get; set; } = null!;
        public DbSet<CountryJourney> CountriesJourneys { get; set; } = null!;
        public DbSet<TownJourney> TownsJourneys { get; set; } = null!;
        public DbSet<ApplicationRole> ApplicationRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .Entity<ApplicationUser>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Entity<ApplicationUser>()
                .Property(u => u.Email)
                .HasMaxLength(60)
                .IsRequired();

            builder
               .Entity<ApplicationUser>()
               .Property(u => u.FirstName)
               .HasMaxLength(60)
               .IsRequired();

            builder
               .Entity<ApplicationUser>()
               .Property(u => u.LastName)
               .HasMaxLength(60)
               .IsRequired();


            builder
                .Entity<ApplicationUserJourney>()
                .HasKey(uj => new { uj.JourneyId, uj.ApplicationUserId });


            builder
                .Entity<CountryJourney>()
                .HasKey(cj => new { cj.JourneyId, cj.CountryId });

            builder
                .Entity<TownJourney>()
                .HasKey(cj => new { cj.JourneyId, cj.TownId });

            if (this.seedDb)
            {
                builder.ApplyConfiguration(new ApplicationUserConfiguration());
                builder.ApplyConfiguration(new CommentConfiguration());
                builder.ApplyConfiguration(new CountryConfiguration());
                builder.ApplyConfiguration(new JourneyConfiguration());
                builder.ApplyConfiguration(new PostConfiguration());
                builder.ApplyConfiguration(new TownConfiguration());
                builder.ApplyConfiguration(new TripConfiguration());
                builder.ApplyConfiguration(new ApplicationUserJourneyConfiguration());
                builder.ApplyConfiguration(new CountryJourneyConfiguration());
                builder.ApplyConfiguration(new RequestConfiguration());
                builder.ApplyConfiguration(new TownJourneyConfiguration());
            }


            base.OnModelCreating(builder);
        }
    }
}
