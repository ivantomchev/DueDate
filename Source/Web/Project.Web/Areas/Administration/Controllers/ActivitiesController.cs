namespace Project.Web.Areas.Administration.Controllers
{
    using Project.Data.UnitOfWork;
    using Project.Web.Areas.Administration.Controllers.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using Project.Common.Extensions;
    using Project.Data.Models;

    using DbModel = Project.Data.Models.Activity;
    using IndexViewModel = Project.Web.Areas.Administration.ViewModels.Activities.ActivityIndexViewModel;
    using InputModel = Project.Web.Areas.Administration.ViewModels.Activities.ActivityInputModel;
    using DeleteViewModel = Project.Web.Areas.Administration.ViewModels.Activities.ActivityDeleteViewModel;
    using Infrastrusture.Extensions;

    public class ActivitiesController : AdminController
    {
        private const int PageSize = 10;

        public ActivitiesController(IProjectData Data)
            :base(Data)
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
            var data = GetData<IndexViewModel>().OrderByDescending(x => x.Id).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_ReadActivitiesPartial", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            model.ClientsList = this.Data.Clients.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();
            model.TasksList = this.Data.ActivityTasks.All().ToSelectList(x => x.Description, x => x.Id).OrderBy(x => x.Text).ToList();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            PopulateSelectedTasks(model.Tasks, model.selectedTasks);

            var dbModel = base.Create<DbModel>(model);

            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }
            model.ClientsList = this.Data.Clients.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();
            model.TasksList = this.Data.ActivityTasks.All().ToSelectList(x => x.Description, x => x.Id).OrderBy(x => x.Text).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            var model = base.GetViewModel<DbModel, InputModel>(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            model.ClientsList = this.Data.Clients.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();
            model.TasksList = this.Data.ActivityTasks.All().ToSelectList(x => x.Description, x => x.Id).OrderBy(x => x.Text).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(InputModel model)
        {
            PopulateSelectedTasks(model.Tasks, model.selectedTasks);

            var dbModel = base.Update<DbModel, InputModel>(model, model.Id);
            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }
            model.ClientsList = this.Data.Clients.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();
            model.TasksList = this.Data.ActivityTasks.All().ToSelectList(x => x.Description, x => x.Id).OrderBy(x => x.Text).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

            return PartialView("_DeleteActivityPartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(DeleteViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id);

            return base.GridOperationAjaxRefreshData();
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

        private void PopulateSelectedTasks(ICollection<ActivityTask> tasks, int[] selectedTasks)
        {
            if (selectedTasks != null)
            {
                foreach (var taskId in selectedTasks)
                {
                    var currentTask = this.Data.ActivityTasks.GetById(taskId);

                    tasks.Add(currentTask);
                }
            }
        }
    }
}