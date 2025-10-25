using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class EditarDto
    {
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "O campo deve possuir um endereço de email válido!")]
        public string? Email { get; set; }

        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres!")]
        public string? Senha { get; set; }
        public string? Setor { get; set; }
        public string? Telefone { get; set; }
    }
}
