namespace ScribeTracker.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;        // For SelectListItem
    using System.Collections.Generic;                // For List<T>
    using System.ComponentModel.DataAnnotations;     // For [Required], validation attributes
    using ScribeTracker.Models;


    public class WorkPenNameViewModel
    {
        public int WorkId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public WorkType Type { get; set; }

        [Required(ErrorMessage = "Please select a pen name.")]
        public int PenNameId { get; set; }

        public List<SelectListItem> PenNames { get; set; } = new();

        public List<SelectListItem> WorkTypes { get; set; } = new();
    }


}
