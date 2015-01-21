using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WebApplication1.Models
{
    /// <summary>
    /// ToDoモデル
    /// </summary>
    public class Todo
    {
        public int id { get; set; }
        [DisplayName("概要")]
        public string summary { get; set; }
        [DisplayName("詳細")]
        public string detail { get; set; }
        [DisplayName("期限")]
        public DateTime limit { get; set; }
        [DisplayName("完了")]
        public bool done { get; set; }
    }
}