namespace API_MVC_Suptech.Entitys
{
    public class Chamados
    {
        public Guid ChamadoId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public int CategoriaId { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
        public required int DataAbertura { get; set; }
        public required string Status { get; set; }
        public required int Prioridade { get; set; }
        public string? SolucaoSugeridaPelaIA { get; set; }
        public required int DataFechamento { get; set; }
        public string? SolucaoAplicada { get; set; }

        //PKchamado não implementado
    }
}
