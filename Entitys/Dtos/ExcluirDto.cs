namespace API_MVC_Suptech.Entitys.Dtos
{
    public class ExcluirDto
    {
        public required Guid UsuarioID { get; set; }

        public required Guid AdministradorID { get; set; }

        public required Guid GerenteId { get; set; }

        public required Guid TecnicoId { get; set; }

        public required Guid ChamadoID { get; set; }

    }
}
