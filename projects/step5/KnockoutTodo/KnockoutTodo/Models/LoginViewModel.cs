﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KnockoutTodo.Models
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