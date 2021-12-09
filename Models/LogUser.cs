using System;
using System.ComponentModel.DataAnnotations;


namespace loginRegDec.Models
{
    public class LogUser
    {
    

        [Required]
        [EmailAddress]
        // email pattern A-Z 0-9 @A-Z.com/.net(A-Z)

        public string lemail{get;set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]

        public string lpassword{get;set;}

        

    }
}