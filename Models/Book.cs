using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên sách")]
        public string Title { get; set; }
        [Display(Name = "Tác giả")]
        public string Author { get; set; }
        [Display(Name = "Nhà xuất bản")]
        public string? Pulisher { get; set; }
        [Display(Name = "Năm xuất bản")]
        public DateTime? PulisherYear { get; set; }

        public virtual ICollection<BorrowedBook>? BorrowedBooks { get; set;}

    }
}
