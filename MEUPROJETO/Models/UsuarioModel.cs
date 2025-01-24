using MEUPROJETO.Enums;

namespace MEUPROJETO.Models
{
    public class UsuarioModel
    {
        //modelo de tabela usuario do banco de dados
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; } // dado que o usuario passara como chave para acesso ao sistema (cpf,email...)
        public string Email  { get; set; } // caso esqueça a senha para resetar
        public PerfilEnum Perfil { get; set; } // Tipo de perfil enum caso seja adm ou padrão
        public string Senha { get; set; } // caso esqueça a senha para resetar
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; } // não é obrigatorio pois na primeira vez de criação nao tem essa data, apenas a de cadastro


    }
}
