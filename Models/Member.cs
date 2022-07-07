using System.ComponentModel.DataAnnotations;

namespace GettoAPI.Models 
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? NickName { get; set; }
        
        public string? FullName { get; set; }

        public string? Gang { get; set; }

        [Required]
        [MaxLength(100)]
        public int Respect { get; set; }
    } 
}