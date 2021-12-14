using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookTrackingApp.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name ="Name Token")]
        [Column(TypeName = "NVARCHAR(250)")]
        [MaxLength(250)]
        public string NameToken { get; set; }

        [Required]        
        [ForeignKey("CategoryTypeModel")]
        public int  Type { get; set; }
        public CategoryTypeModel CategoryTypeModel { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Description { get; set; }


        //RelationShip
        public List<BookModel> BookList { get; set; }
    }
}
