using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthTest.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("ユーザー名")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("パスワード")]
        public string Password { get; set; }
    }
}