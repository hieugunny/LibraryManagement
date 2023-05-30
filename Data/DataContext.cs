using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryManagement.Models.LibraryCards> LibraryCards { get; set; } = default!;

        public DbSet<LibraryManagement.Models.Book>? Book { get; set; }
    public DbSet<LibraryManagement.Models.BorrowedBook>? BorrowedBook { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BorrowedBook>()
            .HasOne(borrow => borrow.Book)
            .WithMany(book => book.BorrowedBooks)
            .HasForeignKey(borrow=> borrow.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BorrowedBook>()
            .HasOne(borrow => borrow.LibraryCards)
            .WithMany(card => card.BorrowedBooks)
            .HasForeignKey(borrow => borrow.CardId) 
            .OnDelete(DeleteBehavior.Restrict);


    }

}
