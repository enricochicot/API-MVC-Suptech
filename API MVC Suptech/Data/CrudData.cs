
using Microsoft.EntityFrameworkCore;

namespace API_MVC_Suptech.Data
{
    public class CrudData : DbContext 
    {
        public CrudData(DbContextOptions<CrudData> options) : base(options)
        {
        
        }
        public DbSet<Entitys.Administrador> Administradores { get; set; }
        public DbSet<Entitys.Gerente> Gerentes { get; set; }
        public DbSet<Entitys.Tecnico> Tecnicos { get; set; }
        public DbSet<Entitys.Usuario> Usuarios { get; set; }
    }
}
