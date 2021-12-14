﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookTrackingApp.Models
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        [MaxLength(250)]
        public string ISBN { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Category")]
        [ForeignKey("CategoryModel")]
        public int Category { get; set; }
        public CategoryModel CategoryModel { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        [MaxLength(250)]
        public string Author { get; set; }
    }
}
