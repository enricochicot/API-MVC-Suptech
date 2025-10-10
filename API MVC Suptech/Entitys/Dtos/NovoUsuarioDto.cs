using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class NovoUsuarioDto
    {
        public required string Nome { get; set; }
    
        public required string Email { get; set; }
    
        public required string Senha { get; set; }

        public required string Setor { get; set; }

        public required string Telefone { get; set; }
    }
}
