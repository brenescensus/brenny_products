using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace brenny_products.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        
        [Column(TypeName = "nvarchar(50)")]
        public string Image { get; set; }


        public int Status{ get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}
