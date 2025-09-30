namespace API_MVC_Suptech.Entitys.Dtos
{
    public class EditarDto
    {
        public required string? Nome { get; set; }
        public required string? Email { get; set; }
        public required string? Senha { get; set; }
        public required string? Setor { get; set; }
        public required int? Telefone { get; set; }
    }
}
