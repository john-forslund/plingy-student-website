using System;
using System.ComponentModel.DataAnnotations;

namespace Plingy.Models
{
    public class Allergies
    {
     
        public int AllergiesID { get; set; }
        public int StudentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Allergy { get; set; }

        public Student Student { get; set; }

    }
}