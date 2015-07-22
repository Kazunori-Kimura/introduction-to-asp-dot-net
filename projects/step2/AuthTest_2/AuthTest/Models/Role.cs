using System.Collections.Generic;

namespace AuthTest.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}