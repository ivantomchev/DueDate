namespace Project.Web.Areas.Administration.ViewModels.Clients
{
    using Project.Data.Models;
    using Project.Web.Infrastructure.Mapping;

    public class ClientIndexViewModel : IMapFrom<Client>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}