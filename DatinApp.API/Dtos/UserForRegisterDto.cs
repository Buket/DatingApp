using System.ComponentModel.DataAnnotations;

namespace DatinApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must enter more than 4 charecters")]
        public string Password { get; set; }
    }
}