using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class NovoUsuarioDto
    {
     [Required]
     public string Nome { get; set; }

     [Required]
     public string Email { get; set; }

     [Required]
     public string Senha { get; set; }
    }
}
