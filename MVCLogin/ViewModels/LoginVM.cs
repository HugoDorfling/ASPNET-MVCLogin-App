using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLogin.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        public string LoginErrorMessage { get; set; }

    }
}