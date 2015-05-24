namespace Project.Web.ViewModels.Activities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Project.Common;
    using Project.Data.Models;
    using Project.Web.Infrastructure.Mapping;

    public class ActivityRecaltulateDueDateViewModel : IMapFrom<Activity>
    {
        [Required]
        [Range(0, 400)]
        public int Duration { get; set; }

        [Required]
        [UIHint("DateTimePicker")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = GlobalConstants.Test, ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DueDateFormat)]
        public DateTime DueDate { get; set; }
    }
}