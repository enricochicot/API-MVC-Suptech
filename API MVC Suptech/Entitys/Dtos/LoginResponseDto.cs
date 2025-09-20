namespace API_MVC_Suptech.Entitys.Dtos
{
    public class LoginResponseDto
    {
        public Guid UsuarioId { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; } // Adiciona o token JWT à resposta
    }
}
