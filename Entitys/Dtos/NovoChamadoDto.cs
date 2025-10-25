using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class NovoChamadoDto
    {
        [Required(ErrorMessage = "Este campo deve possuir o nome do solicitante")]
        public required string NomeDoUsuario { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public required string EmailDoUsuario { get; set; }

        [Required(ErrorMessage = "Este campo deve possuir o setor do solicitante")]
        public required string SetorDoUsuario { get; set; }

        [Required(ErrorMessage = "Este campo deve possuir o título do chamado")]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "Este campo deve possuir a descrição do chamado")]
        public required string Descricao { get; set; }

        [Required(ErrorMessage = "Este campo deve possuir a prioridade do chamado")]
        public required string Prioridade { get; set; }
    }
}
