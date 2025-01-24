using MEUPROJETO.Models;
using Microsoft.EntityFrameworkCore;


namespace MEUPROJETO.Data
{
    public class BancoContext : DbContext
    {
        /*Configurando o construtor com o DbContextOptions como parametro de entrada e passa para a classe DB context
         essa informação
        */
        public BancoContext(DbContextOptions<BancoContext> options)
     : base(options)
        {
        }
        /*Configurando a entidade ContatoModel para dentro do contexto
         No caso criando a tabela contatos*/

        public DbSet<ContatoModel> Contatos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}

