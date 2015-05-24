namespace Project.Data.UnitOfWork
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;

    using Project.Data.Common.Repository;
    using Project.Data.Models;

    public class ProjectData : IProjectData
    {
        private readonly IApplicationDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public ProjectData(IApplicationDbContext context)
        {
            this.context = context;
        }

        public IApplicationDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<Activity> Activities
        {
            get { return this.GetRepository<Activity>(); }
        }

        public IRepository<ActivityTask> ActivityTasks
        {
            get { return this.GetRepository<ActivityTask>(); }
        }

        public IRepository<Client> Clients
        {
            get { return this.GetRepository<Client>(); }
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
