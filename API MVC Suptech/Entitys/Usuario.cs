using System.ComponentModel.DataAnnotations;

namespace API_MVC_Suptech.Entitys
{
    public class Usuario
    {
        [Key]
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public required string Nome { get; set; }


        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(20, MinimumLength = 12, ErrorMessage = "A senha deve ter entre 12 e 20 caracteres.")]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "O setor é obrigatório.")]
        [StringLength(45, ErrorMessage = "O setor não pode exceder 45 caracteres.")]
        public required string Setor { get; set; }

        public Guid? GerenteId { get; set; }

        public DateTimeOffset CreationDate { get; set; }
    }
}
