namespace Project.Web.Areas.Administration.ViewModels.Clients
{
    using System.ComponentModel.DataAnnotations;

    using Project.Data.Models;
    using Project.Web.Infrastructure.Mapping;

    public class ClientInputModel : IMapFrom<Client>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}