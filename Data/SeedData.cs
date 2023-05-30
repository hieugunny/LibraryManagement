using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Reflection;
using System.Security.Principal;

namespace LibraryManagement.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<DataContext>>()))
            {
                if(!context.Book.Any() || !context.LibraryCards.Any() || !context.BorrowedBook.Any() ) {
                    //create books
                    var b1 = new Book
                    {
                        Title = "Toan roi rac 1",
                        Author = "Le Trung Hieu",
                        Pulisher = "NXB GIAO DUC",
                        PulisherYear = DateTime.Parse("08/18/2018 07:22:16")
                    };
                    var b2 = new Book
                    {
                        Title = "Toan roi rac 2",
                        Author = "Le Trung Hieu",
                        Pulisher = "NXB Kim Dong",
                        PulisherYear = DateTime.Parse("08/18/2018 07:22:16")
                    };
                    var b3 = new Book
                    { 
                        Title = "Toan roi rac 3",
                        Author = "Le Trung Hieu",
                        Pulisher = "NXB GIAO DUC Da Nang",
                        PulisherYear = DateTime.Parse("08/18/2018 07:22:16")
                    };

                    var l1 = new LibraryCards
                    {
                        Id = "lc1",
                        Name = "Nguyen Tan Phat",
                        ClassId = "20D1TH06",
                        DateOfBirth = DateTime.Parse("08/18/2002 07:22:16"),
                        Address = "190 phan van han, p15,q Binh Thanh, tp HCM",
                        Major = "CNTT"

                    };
                    var l2 = new LibraryCards
                    {
                        Id = "lc2",
                        Name = "Le Trong Huu",
                        ClassId = "20D1TH06",
                        DateOfBirth = DateTime.Parse("08/18/2002 07:22:16"),
                        Address = "190 phan van han, p Tay Thanh,q Tan Phu, tp HCM",
                        Major = "QTKD"

                    };
                    var l3 = new LibraryCards
                    {
                        Id = "lc3",
                        Name = "Nguyen Kieu My Tam",
                        ClassId = "20D1TH06",
                        DateOfBirth = DateTime.Parse("08/18/2002 07:22:16"),
                        Address = "140 Dien Bien Phu, p15, q Binh Thaanh, tp HCM",
                        Major = "Luat"

                    };



                    context.Book.AddRange(
                        b1,b2, b3
                    );
                    context.SaveChanges();
                    context.LibraryCards.AddRange(
                        l1,l2,l3
                    );
                    context.SaveChanges();
                    var idbook1 = from b in context.Book where b.Id == 1 select b.Id;
                    var idbook2 = from b in context.Book where b.Id == 1 select b.Id;
                    var lcid1 = from b in context.LibraryCards where b.Id == "lc1" select b.Id;
                    var lcid2 = from b in context.LibraryCards where b.Id == "lc2" select b.Id;
                    context.BorrowedBook.AddRange(
                        new BorrowedBook {
                            Book = b1,
                            LibraryCards = l1,
                            BorrowDate = DateTime.Today,
                            DueDate = DateTime.Today,

                        },
                        new BorrowedBook
                        {
                            Book = b2,
                            LibraryCards = l2,
                            BorrowDate = DateTime.Today,
                            DueDate = DateTime.Today,

                        },
                        new BorrowedBook
                        {
                            Book = b1,
                            LibraryCards = l2,
                            BorrowDate = DateTime.Today,
                            DueDate = DateTime.Today,

                        }
                    );
                    context.SaveChanges();
                        
                }

            }
        }
    }
}
