using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.Models
{
    public class UserT
    {
        [Key]
        public int Id { get; set; }
        [Column("Username", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("Usermail", TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column("password", TypeName = "varchar(10)"),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
