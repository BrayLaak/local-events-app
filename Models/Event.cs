using System;
using System.ComponentModel.DataAnnotations;

namespace local_events_app.Models
{
    public class Event
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^[A-Za-z0-9 ]+$", ErrorMessage = "Title should only contain letters, numbers, and spaces.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [RegularExpression(@"^\d{2}-\d{2}-\d{4}$", ErrorMessage = "Date should be in the format DD-MM-YYYY.")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [RegularExpression("^[A-Za-z0-9 ]+$", ErrorMessage = "Description should only contain letters, numbers, and spaces.")]
        public string Description { get; set; }

        [RegularExpression("^[A-Za-z0-9 ]*$", ErrorMessage = "Location should only contain letters, numbers, and spaces.")]
        public string? Location { get; set; }
    }
}
