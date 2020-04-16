using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Plingy.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Date)]  // So that only date, not time, matters.
        public DateTime JoinDate { get; set; }
        
        [Required]
        public bool ActiveStudent { get; set; }
        public string Address { get; set; }

        public ICollection<Allergies> StudentsAllergies { get; set; } // Holds all allergy entities related to the respective student.
    }
}