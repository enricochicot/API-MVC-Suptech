using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class EditarTecnicoDto
    {
        
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email { get; set; }

        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres.")]
        public string? Senha { get; set; }

        public string? Especialidade { get; set; }

        public string? Telefone { get; set; }
    }
}
