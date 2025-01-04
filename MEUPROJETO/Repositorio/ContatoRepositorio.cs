using MEUPROJETO.Data;
using MEUPROJETO.Models;

namespace MEUPROJETO.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        //Criando uma variavel para conseguir acessar os metodos do bancoContext
        private readonly BancoContext _bancoContext;
        //Construtor que recebe como parametro a injetação do bancocontext
        public ContatoRepositorio(BancoContext bancoContext)
        {
            //a variavel que criei la em cima recebe a instancia que vem por parametro
            _bancoContext = bancoContext;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            // Inserindo(gravar) os dados no banco, injetado do contexto
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }
    }
}
