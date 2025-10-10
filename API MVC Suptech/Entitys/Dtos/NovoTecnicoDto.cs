namespace API_MVC_Suptech.Entitys.Dtos
{
    public class NovoTecnicoDto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string Especialidade { get; set; }
        public required string Telefone { get; set; }
    }
}
