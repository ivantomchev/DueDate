namespace Project.Web.Areas.Administration.ViewModels.Activities
{
    using AutoMapper;

    using Project.Data.Models;
    using Project.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class ActivityIndexViewModel : IMapFrom<Activity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Display(Name = "Client")]
        public string ClientName { get; set; }

        public int Duration { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Activity, ActivityIndexViewModel>()
                .ForMember(d => d.ClientName, opt => opt.MapFrom(s => s.Client.Name));
        }
    }
}