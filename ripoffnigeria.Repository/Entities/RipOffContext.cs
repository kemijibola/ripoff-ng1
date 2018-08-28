using System.Data.Entity;
using ripoffnigeria.DTO;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ripoffnigeria.Repository.Entities
{
    public class RipOffContext : DbContext
    {
        public RipOffContext()
            : base("name=RipOffContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<RipOffContext, RipOffMigrationsConfiguration>()
                );
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Report>()
                .HasRequired(c => c.City)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rebuttal>()
                        .HasRequired(e => e.State)
                        .WithMany()
                        .HasForeignKey(e => e.StateId)
                        .WillCascadeOnDelete(false);

                modelBuilder.Entity<Report>()
                .HasRequired(e => e.State)
                .WithMany()
                .HasForeignKey(e => e.StateId)
                .WillCascadeOnDelete(false);

                modelBuilder.Entity<Report>()
                .HasRequired(c => c.State)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Report>()
                .HasRequired(c => c.Category)
                .WithMany()
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ClientMeetingRequest>()
                .HasRequired(c => c.FirmRegion)
                .WithMany()
                .WillCascadeOnDelete(false);

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<Rebuttal> Rebuttals { get; set; }
        public DbSet<RebuttalImage> RebuttalImages { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportImage> ReportImages { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<RipOffLawyer> RipOffLawyers { get; set; }
        public DbSet<LawCategory> LawCategories { get; set; }
        public DbSet<FirmCategory> FirmCategories { get; set; }
        public DbSet<FirmImage> FirmImages { get; set; }
        public DbSet<LawTypeCategory> LawTypeCategories { get; set; }
        public DbSet<RipOffFirm> RipOffFirms { get; set; }
        public DbSet<trackUser> trackUsers { get; set; }
        public DbSet<FirmComment> FirmComments { get; set; }
        public DbSet<thankYouEmail> thankYouEmails { get; set; }
        public DbSet<FirmRegion> FirmRegions { get; set; }
        public DbSet<LawFirm> LawFirms { get; set; }
        public DbSet<CaseUpdate> CaseUpdates { get; set; }
        public DbSet<ClientLawsuit> ClientLawsuits { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ReportBug> ReportBugs { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<ClientMeetingRequest> ClientMeetingRequests { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<RejectionReason> RejectionReasons { get; set; }
        public DbSet<ReportRejection> ReportRejections { get; set; }
    }
}