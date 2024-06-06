using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model
{
    public class UserRoles
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        public int UserId { get; set; }
      //  public string Username { get; set; }
        [Required]
        [ForeignKey("RoleId")]
        public virtual Roles Roles { get; set; }
        public int RoleId { get; set; }
      //  public string RoleName { get; set; }
    }
}
