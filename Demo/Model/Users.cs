using System.ComponentModel.DataAnnotations;

namespace Demo.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set;}
        [Required]
        [StringLength(50)]
        public string Password { get; set;}

        public string? Email {  get; set;}
        public string? Mobile {  get; set;}
        public DateTime? CreationDate { get; set;} = DateTime.Now;
        public DateTime? UpdationTime { get; set;}
    }
}
