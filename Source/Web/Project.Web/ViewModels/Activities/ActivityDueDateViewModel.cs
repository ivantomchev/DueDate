namespace Project.Web.ViewModels.Activities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Project.Common;

    public class ActivityDueDateViewModel
    {
        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DueDateFormat)]
        public DateTime DueDate { get; set; }
    }
}