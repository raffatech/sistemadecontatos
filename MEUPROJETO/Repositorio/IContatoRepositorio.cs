using MEUPROJETO.Models;

namespace MEUPROJETO.Repositorio
{
    public interface IContatoRepositorio
        //Criar todos os metodos como contrato para nosso contatoRepositorio
    {
        ContatoModel Adicionar(ContatoModel contato); //metodo que recebe como parametro um objeto do tipo ContatoModel e retorna ele quando adicionar no banco
    }
}
