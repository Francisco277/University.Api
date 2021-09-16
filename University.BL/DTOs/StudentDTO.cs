using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BL.DTOs
{
    public class StudentDTO
    {

        public int ID { get; set; }
        [Required(ErrorMessage ="The Fiel Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Fiel First Name is required")]
        public string FirstMidName { get; set; }
        [Required(ErrorMessage = "The Fiel EnrollmenDate is required")]
        public DateTime EnrollmentDate { get; set; }
        public string FullName
        {
            get { return string.Format("{0}, {1}", LastName, FirstMidName); }
        }
    }
}