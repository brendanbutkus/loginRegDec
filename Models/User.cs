using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginRegDec.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]

        public string username { get; set; }

        [Required]
        [EmailAddress]
        // email pattern A-Z 0-9 @A-Z.com/.net(A-Z)

        public string email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]

        public string password { get; set; }

        // dont forget created at and updated at
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("password")]

        public string confirm {get;set;}



    }
}