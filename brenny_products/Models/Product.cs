using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace brenny_products.Models
{
    public class Product
    {
        [Key]
        [DisplayName("ID")]
        public int ProductId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Image { get; set; }

        public int Price{ get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int Userid { get; set; }
        public User User { get; set; }





    }
}
