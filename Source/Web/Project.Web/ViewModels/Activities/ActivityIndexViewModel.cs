namespace Project.Web.ViewModels.Activities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Project.Data.Models;
    using Project.Web.Infrastructure.Mapping;
    using Project.Common;

    public class ActivityIndexViewModel : IMapFrom<Activity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Display(Name = "Client")]
        public string ClientName { get; set; }

        public int Duration { get; set; }

        [Display(Name="Start Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeDueDateFormatString)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeDueDateFormatString)]
        public DateTime DueDate { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Activity, ActivityIndexViewModel>()
                .ForMember(d => d.ClientName, opt => opt.MapFrom(s => s.Client.Name));
        }
    }
}