using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BL.DTOs
{
  public  class CourseDTO
    {
        [Required(ErrorMessage = "The ID es Required")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "The Title es Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Credits es Required")]

        public int Credits { get; set; }
    }
}
