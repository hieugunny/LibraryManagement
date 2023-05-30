
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BorrowedBook
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên sách")]
        public int BookId { get; set; }
        [Required] 
        [Display(Name = "Mã thẻ")]
        public string CardId { get; set; }
        [Display(Name = "Ngày mượn")]
        public DateTime BorrowDate { get; set; }
        [Display(Name = "Hạn trả")]
        public DateTime DueDate{ get; set; }
        [Display(Name = "Ngày trả")]
        public DateTime? ReturnDate { get; set; }

        public virtual Book? Book { get; set; }
        public virtual LibraryCards? LibraryCards { get; set; }
    }
}
