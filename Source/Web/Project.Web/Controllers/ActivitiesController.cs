namespace Project.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Project.Data.UnitOfWork;

    using DbModel = Project.Data.Models.Activity;
    using IndexViewModel = Project.Web.ViewModels.Activities.ActivityIndexViewModel;
    using DetailedViewModel = Project.Web.ViewModels.Activities.ActivityDetailsViewModel;
    using RecaltulateViewModel = Project.Web.ViewModels.Activities.ActivityRecaltulateDueDateViewModel;
    using Project.Web.ViewModels.Activities;

    public class ActivitiesController : BaseEntityController
    {
        private const int PageSize = 10;

        public ActivitiesController(IProjectData Data)
            : base(Data)
        {
        }

        public ActionResult Index()
        {
            var data = this.GetData<IndexViewModel>();

            return View(data);
        }

        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);
            var count = (double)GetData<IndexViewModel>().Count();
            var data = GetData<IndexViewModel>().OrderByDescending(x => x.Id).Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            foreach (var item in data)
            {
                var duration = item.Duration * 60;
                item.DueDate = GetDueDate(item.StartDate, duration);
            }

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_ReadActivitiesPartial", data);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var model = base.GetViewModel<DbModel, DetailedViewModel>(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            var duration = model.Duration * 60;
            model.DueDate = GetDueDate(model.StartDate, duration);
            return View(model);
        }

        [HttpGet]
        public ActionResult DueDateRecalculate(int? id)
        {
            var model = base.GetViewModel<DbModel, RecaltulateViewModel>(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            var duration = model.Duration * 60;
            model.DueDate = GetDueDate(model.StartDate, duration);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDueDate(RecaltulateViewModel model)
        {
            var duration = model.Duration * 60;
            model.DueDate = GetDueDate(model.StartDate, duration);

            var date = new ActivityDueDateViewModel();
            date.DueDate = model.DueDate;
            return PartialView("_DueDatePartial", date);
        }

        private DateTime GetDueDate(DateTime startDate, int duration)
        {
            if (duration == 0)
            {
                return startDate;
            }

            while (startDate.Hour < 16 && duration > 0)
            {
                startDate = startDate.AddMinutes(1);
                duration--;
            }

            //Now the time is 16:00

            if (duration > 0)
            {
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 8, 30, 0);
                startDate = startDate.AddDays(1);
                //Now it's next day 8:30

                if (startDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    startDate = startDate.AddDays(2);
                }
                if (startDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    startDate = startDate.AddDays(1);
                }
                //Now it's Monday


                startDate = GetDueDate(startDate, duration);

            }
            return startDate;

        }
        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Activities");
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Activities.All().Project().To<TViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Activities.GetById(id) as T;
        }
    }
}