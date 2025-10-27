using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class EditarChamadoDto
    {
        
        public string? NomeDoUsuario { get; set; }

        [EmailAddress(ErrorMessage = "O campo deve possuir um email válido!")]
        public string? EmailDoUsuario { get; set; }
        public string? SetorDoUsuario { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Prioridade { get; set; }
    }
}
