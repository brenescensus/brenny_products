using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace brenny_products.Models
{
    public class User
    {
        [Key]
        [DisplayName("ID")]
        public int Userid { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Image { get; set; }

        [Column(TypeName = "nvarchar(50)")]

        public string Contact { get; set; }

       
    }
}
