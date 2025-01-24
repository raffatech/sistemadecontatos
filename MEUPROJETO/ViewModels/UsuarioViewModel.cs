using MEUPROJETO.Enums;
using System.ComponentModel.DataAnnotations;

namespace MEUPROJETO.Models
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            Id = 0;
            Perfil = null;
            Nome = "";
            Login = string.Empty;
            Email = string.Empty;
            Senha = string.Empty;
            DataCadastro = DateTime.Now;
            DataAtualizacao = null;
        }

        //modelo de tabela usuario do banco de dados
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; } // dado que o usuario passara como chave para acesso ao sistema (cpf,email...)
        [Required(ErrorMessage = "O Email é obrigatório")]
        public string Email { get; set; } // caso esqueça a senha para resetar
        [Required(ErrorMessage = "O Perfil é obrigatório")]
        public PerfilEnum? Perfil { get; set; } // Tipo de perfil enum caso seja adm ou padrão

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        public string Senha { get; set; } // caso esqueça a senha para resetar
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; } // pode ser null pois na primeira vez de criação nao tem essa data, apenas a de cadastro


    }
}
