using API_MVC_Suptech.Entitys;

namespace API_MVC_Suptech.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

    }
}
