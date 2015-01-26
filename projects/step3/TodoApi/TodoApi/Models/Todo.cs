using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApi.Models
{
    public class Todo
    {
        public int id { get; set; }
        public string summary { get; set; }
        public string detail { get; set; }
        public DateTime limit { get; set; }
        public bool done { get; set; }
    }
}