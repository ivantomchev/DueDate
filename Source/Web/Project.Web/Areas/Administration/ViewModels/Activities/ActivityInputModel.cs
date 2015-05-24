namespace Project.Web.Areas.Administration.ViewModels.Activities
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Project.Data.Models;
    using Project.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class ActivityInputModel : IMapFrom<Activity>, IHaveCustomMappings
    {
        public ActivityInputModel()
        {
            this.Tasks = new HashSet<ActivityTask>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Description { get; set; }

        [Required]
        [Range(0, 400)]
        [UIHint("DurationInHours")]
        public int Duration { get; set; }

        [Required]
        //[Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        [UIHint("DateTimePicker")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }

        public IEnumerable<SelectListItem> ClientsList { get; set; }

        [Display(Name = "Tasks")]
        public int[] selectedTasks { get; set; }

        public IEnumerable<SelectListItem> TasksList { get; set; }

        public ICollection<ActivityTask> Tasks { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Activity, ActivityInputModel>()
                .ForMember(d => d.selectedTasks, opt => opt.MapFrom(s => s.Tasks.Select(x => x.Id)));
        }
    }
}