using System.ComponentModel.DataAnnotations;

namespace GettoAPI.Dtos 
{
    public class MemberCDto
    {
        [Required]
        public string? NickName { get; set; }
        
        public string? FullName { get; set; }

        public string? Gang { get; set; }

        [Required]
        [MaxLength(100)]
        public int Respect { get; set; }
    } 
}