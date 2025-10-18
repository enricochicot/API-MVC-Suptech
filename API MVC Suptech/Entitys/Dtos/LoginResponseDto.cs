namespace API_MVC_Suptech.Entitys.Dtos
{
    public class LoginResponseDto
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
