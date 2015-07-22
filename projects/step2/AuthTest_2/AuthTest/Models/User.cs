using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthTest.Models
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("名前")]
        [Required]
        public string UserName { get; set; }

        [DisplayName("パスワード")]
        [Required]
        public string Password { get; set; }

        [DisplayName("役割")]
        public virtual ICollection<Role> Roles { get; set; }

        [DisplayName("Todo")]
        public virtual ICollection<Todo> Todoes { get; set; }
    }
}