using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys
{
    public class Gerente
    {
        [Key]
        public Guid GerenteID { get; set; }
        public required string Nome { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [MinLength(6)]
        public required string Senha { get; set; }
        public required string Setor { get; set; }

        public required string Telefone { get; set; }
    }
}
