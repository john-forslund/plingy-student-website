using System;
using System.Linq;
using System.Collections.Generic;

namespace Plingy.Models
{
    public class StudentAllergiesViewModel
    {
/*
        public int StudentID { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public bool ActiveStudent { get; set; }
        public string Address { get; set; }



        public IList<Allergies> Allergies { get; set; }
*/
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Allergies> Allergies { get; set; }
    }
}