using System.Data.Entity.Migrations;

namespace ripoffnigeria.Repository.Entities
{
    public class RipOffMigrationsConfiguration : DbMigrationsConfiguration<RipOffContext>
    {
        public RipOffMigrationsConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }


        protected override void Seed(RipOffContext context)
        {
            base.Seed(context);
                #if DEBUG

                #endif
            //var manager = new UserManager<ApplicationUser>(
            //    new UserStore<ApplicationUser>(
            //        new ApplicationDbContext()));



        }


    }
}
