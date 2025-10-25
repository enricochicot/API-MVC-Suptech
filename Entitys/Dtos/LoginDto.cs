using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        public required string Email { get; set; }

        [MinLength(6)]
        public required string Senha { get; set; }
    }
}
