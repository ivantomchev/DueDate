﻿namespace Project.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Project.Data.UnitOfWork;
    using Project.Web.Areas.Administration.Controllers.Base;

    using DbModel = Project.Data.Models.ActivityTask;
    using IndexViewModel = Project.Web.Areas.Administration.ViewModels.Tasks.TaskIndexViewModel;
    using InputModel = Project.Web.Areas.Administration.ViewModels.Tasks.TaskInputModel;
    using DeleteViewModel = Project.Web.Areas.Administration.ViewModels.Tasks.TaskIndexViewModel;

    public class TasksController : AdminController
    {
        private const int PageSize = 10;

        public TasksController(IProjectData Data)
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
            var data = GetData<IndexViewModel>().OrderByDescending(x => x.Id).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_ReadTasksPartial", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            var dbModel = base.Create<DbModel>(model);

            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }
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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(InputModel model)
        {
            var dbModel = base.Update<DbModel, InputModel>(model, model.Id);
            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

            return PartialView("_DeleteTaskPartial", model);
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
            return Url.Action("ReadData", "Tasks");
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.ActivityTasks.All().Project().To<TViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.ActivityTasks.GetById(id) as T;
        }
    }
}