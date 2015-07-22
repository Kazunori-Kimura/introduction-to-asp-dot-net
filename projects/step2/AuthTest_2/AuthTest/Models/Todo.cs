using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthTest.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [DisplayName("タイトル")]
        [Required]
        public string Title { get; set; }

        [DisplayName("内容")]
        public string Detail { get; set; }

        [DisplayName("完了")]
        public bool Done { get; set; }

        [DisplayName("担当者")]
        public virtual User User { get; set; }
    }
}