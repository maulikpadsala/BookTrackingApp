using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookTrackingApp.Models
{
    public class CategoryTypeModel
    {
        [Key]
        public int CategoryTypeId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        [MaxLength(250, ErrorMessage = "Minimum Length is 250")]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        [MaxLength(250, ErrorMessage = "Minimum Length is 250")]
        public string Name { get; set; }


        //RelationShip
        public List<CategoryModel> CategoryList { get; set; }
    }
}
