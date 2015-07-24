using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("タイトル")]
        public string Title { get; set; }

        [Required]
        [DisplayName("内容")]
        public string Detail { get; set; }

        [DisplayName("完了")]
        public bool Done { get; set; }

        public virtual User User { get; set; }
    }
}