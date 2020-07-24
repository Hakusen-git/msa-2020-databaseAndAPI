using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentSIMS
{
    public class Student
    {
        public Student()
        {
            this.addresses = new List<Address>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int studentID { get; set; }
        [Required, MaxLength(100)]
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Required]
        public string emailAddress { get; set; }
        public int phoneNumber { get; set; }


        public DateTime? timeCreated { get; set; }


        public virtual IList<Address> addresses { get; set; }



    }

   
}
