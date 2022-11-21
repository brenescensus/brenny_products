using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace brenny_products.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required(ErrorMessage ="username is required")]
        [Column(TypeName = "nvarchar(50)")]
        public string Username { get; set; }

        [Required(ErrorMessage = "password is required")]

        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
    }
}
