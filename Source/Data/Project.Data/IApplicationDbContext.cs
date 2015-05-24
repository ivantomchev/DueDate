namespace Project.Data
{
    using Project.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IApplicationDbContext
    {
        IDbSet<Activity> Activities { get; set; }

        IDbSet<ActivityTask> ActivityTasks { get; set; }

        IDbSet<Client> Clients { get; set; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
