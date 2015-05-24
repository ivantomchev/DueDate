namespace Project.Web.Controllers
{
    using System.Web.Mvc;

    using Project.Data.UnitOfWork;

    public class BaseController : Controller
    {
        protected IProjectData Data { get; private set; }

        public BaseController(IProjectData data)
        {
            this.Data = data;
        }
    }
}