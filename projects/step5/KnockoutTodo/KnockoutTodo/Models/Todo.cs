using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KnockoutTodo.Models
{
    public class Todo
    {
        public int id { get; set; }

        [Required]
        public string summary { get; set; }

        public string detail { get; set; }

        public DateTime limit { get; set; }

        public bool done { get; set; }
    }
}