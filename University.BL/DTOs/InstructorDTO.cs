using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BL.DTOs
{
   public class InstructorDTO
    {
        public int ID { get; set; }
        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime HireDate { get; set; }

        [JsonProperty("Full Name")]
        public string FullName
        {
            get { return string.Format("{0}, {1}", LastName, FirstMidName); }
        }
    }
}
