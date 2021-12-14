using BookTrackingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTrackingApp.Database
{
    public class BookTrackingDataContext: DbContext
    {
        public BookTrackingDataContext(DbContextOptions options)
      : base(options)
        { }

        public DbSet<BookModel> Book { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<CategoryTypeModel> CategoryType { get; set; }
    }
}
