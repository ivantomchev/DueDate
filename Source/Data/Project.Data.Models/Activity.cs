namespace Project.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Activity
    {
        private ICollection<ActivityTask> tasks;

        public Activity()
        {
            this.tasks = new HashSet<ActivityTask>();
        }

        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }

        public DateTime StartDate { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<ActivityTask> Tasks 
        {
            get
            {
                return this.tasks;
            }
            set
            {
                this.tasks = value;
            }
        }
    }
}
