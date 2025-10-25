using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class NovoTecnicoDto
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres.")]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "A especialidade do técnico é obrigatória!")]
        public required string Especialidade { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório!")]
        public required string Telefone { get; set; }
    }
}
