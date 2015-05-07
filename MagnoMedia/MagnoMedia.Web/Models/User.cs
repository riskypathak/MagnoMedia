using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MagnoMedia.Web.Models
{
    public class User
    {
        [Required(ErrorMessage = "Please Provide UserName", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}