using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace onlinecarrer.Models
{
    public class Companylogin
    {
        
       
        [Required(ErrorMessage = "Please enter User Id")]
        [Display(Name ="Comapny Id")]
        public int comapnyid { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Password")]
        public string compPassword { get; set; }
    }
}