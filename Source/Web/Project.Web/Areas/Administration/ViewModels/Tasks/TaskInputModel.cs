namespace Project.Web.Areas.Administration.ViewModels.Tasks
{
    using System.ComponentModel.DataAnnotations;

    using Project.Data.Models;
    using Project.Web.Infrastructure.Mapping;

    public class TaskInputModel : IMapFrom<ActivityTask>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Description { get; set; }
    }
}