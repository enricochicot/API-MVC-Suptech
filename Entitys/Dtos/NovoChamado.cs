using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys.Dtos
{
    public class NovoChamado
    {
        public required string NomeDoUsuario { get; set; }

        [EmailAddress]
        public required string EmailDoUsuario { get; set; }
        public required string SetorDoUsuario { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
        public required string Prioridade { get; set; }
    }
}
