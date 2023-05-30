using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class LibraryCards
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Lớp")]
        public string ClassId { get; set; }
        [Required]
        [Display(Name = "Tên ")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Ngành")]
        public string Major { get; set; }
        public virtual ICollection<BorrowedBook>? BorrowedBooks { get; set; }

    }
}
