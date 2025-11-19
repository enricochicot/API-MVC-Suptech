using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MVC_Suptech.Entitys
{
    public class Chamado
    {
        [Key]
        public Guid ChamadoID { get; set; }
        public required string NomeDoUsuario { get; set; }

        [EmailAddress]
        public required string EmailDoUsuario { get; set; }
        public required string SetorDoUsuario { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
        public required string Prioridade { get; set; }
        public required string Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DataAbertura { get; set; } = DateTime.Now;

        // Resposta do técnico pode ser nula inicialmente
        public string? Resposta { get; set; }

        // Relacionamento com a entidade Técnico (opcional)

    }
}
