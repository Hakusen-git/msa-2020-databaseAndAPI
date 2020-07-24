using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSIMS
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("student")]
        public int studentID { get; set; }
        [Required]
        public int streetNumber { get; set; }
        [Required, StringLength(100)]
        public string street { get; set; }
        [Required, StringLength(100)]
        public string suburb { get; set; }
        [Required, StringLength(100)]
        public string city { get; set; }
        [Required]
        public int postCode { get; set; }
        [Required, StringLength(100)]
        public string country { get; set; }

        [JsonIgnore]
        public virtual Student student { get; set; }
    }
}
