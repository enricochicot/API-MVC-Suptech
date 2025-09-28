using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys
{
    public class Administrador
    {
        [Key]
        public Guid AdministradorID { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string Setor { get; set; }
    }
}
