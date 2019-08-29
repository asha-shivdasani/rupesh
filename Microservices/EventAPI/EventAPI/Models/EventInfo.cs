using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventAPI.Models
{
    [Table("Events")]
    public class EventInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string EventTitle { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Organizer is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimum 3 and maximum 50 characters allowed")]
        public string Organizer { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [DataType(DataType.Url)]
        [Required(ErrorMessage = "RegistrationUrl is required")]
        public string RegistrationUrl { get; set; }


    }
}
