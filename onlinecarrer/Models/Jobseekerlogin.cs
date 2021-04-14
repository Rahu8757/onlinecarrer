using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace onlinecarrer.Models
{
    public class Jobseekerlogin
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter User Name"), MaxLength(30)]
        [Display(Name = "User Name")]
        public string JsName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Password")]
        public string JsPassword { get; set; }
    }
}