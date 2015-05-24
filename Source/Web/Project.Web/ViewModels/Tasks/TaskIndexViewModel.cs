namespace Project.Web.ViewModels.Tasks
{
    using Project.Data.Models;
    using Project.Web.Infrastructure.Mapping;

    public class TaskIndexViewModel : IMapFrom<ActivityTask>
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}