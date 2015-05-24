namespace Project.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Project.Data.Models;
    using Project.Data.Migrations;

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("InnoSysDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<Activity> Activities { get; set; }

        public virtual IDbSet<ActivityTask> ActivityTasks { get; set; }

        public virtual IDbSet<Client> Clients { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
