using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class EditarDto
    {
        public string? Nome { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [MinLength(6)]
        public string? Senha { get; set; }
        public string? Setor { get; set; }
        public string? Telefone { get; set; }
    }
}
