namespace Project.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ActivityTask
    {
        private ICollection<Activity> activities;

        public ActivityTask()
        {
            this.activities = new HashSet<Activity>();
        }

        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Activity> Activities
        {
            get
            {
                return this.activities;
            }
            set
            {
                this.activities = value;
            }
        }
    }
}
