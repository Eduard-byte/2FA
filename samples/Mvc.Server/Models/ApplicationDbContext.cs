using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Server.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventParameter> EventParameters { get; set; }
        public virtual DbSet<EventParameterGroup> EventParameterGroups { get; set; }
        public virtual DbSet<EventProgram> EventPrograms { get; set; }
        public virtual DbSet<LogicalZonePackage> LogicalZonePackages { get; set; }
        public virtual DbSet<LogicalZoneRequest> LogicalZoneRequests { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageProgramPoint> PackageProgramPoints { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<ParameterValue> ParameterValues { get; set; }
        public virtual DbSet<ProgramPoint> ProgramPoints { get; set; }
        public virtual DbSet<ProgramPointBranch> ProgramPointBranches { get; set; }
        public virtual DbSet<PhysicalZoneProgramPoint> PhysicalZoneProgramPoints { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestProgramPoint> RequestProgramPoints { get; set; }
        public virtual DbSet<RequestStatusHistory> RequestStatusStories { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }


        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                   .Entity<Parameter>()
                   .Property(e => e.Type)
                   .HasConversion<string>();

            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
