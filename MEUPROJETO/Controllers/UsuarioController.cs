using MEUPROJETO.Data;
using MEUPROJETO.Models;
using MEUPROJETO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MEUPROJETO.Controllers
{
    //[Route("Contato")] // Rota fixa para a controller Contato
    // [ApiController] // Definindo que é uma controladora de API
    public class UsuarioController : Controller
    {

        private readonly BancoContext _bancoContext;

        // criei construtor para injetar o banco e inserir os registros direto
        public UsuarioController(BancoContext bancoContext)
        {

            _bancoContext = bancoContext;
        }

        // Método para exibir a listagem de contatos
        public IActionResult ListagemUsuario()
        {
            /*exibindo na tela uma lista do tipo ContatoModel
              que pega do banco a tabela Contatos e lista*/
            List<UsuarioModel> usuarios = _bancoContext.Usuarios.ToList();

            return View(usuarios);
        }

        // Método para exibir a tela de criação de um novo contato

        public IActionResult CriarUsuarioView()
        {
            return View();
        }

        [HttpGet]
        // Método para exibir a tela de confirmação de exclusão de um contato
        public IActionResult AlertarExclusaoView(int id)
        {
            UsuarioModel? UsuarioaSerapagado = _bancoContext.Usuarios
             .Where(w => w.Id == id)
             .FirstOrDefault();

            // Mapeando o UsuarioaSerapagado da ContatoModel para ContatoViewModel
            UsuarioViewModel usuarioView = new UsuarioViewModel
            {
                Id = UsuarioaSerapagado.Id
            };

            return View(usuarioView);
        }

        public IActionResult Apagar(int id)
        {
            // Busca o contato no banco de dados pelo Id usando FirstOrDefault
            UsuarioModel? UsuarioBanco = _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);

            // Verifica se o contato existe no banco
            if (UsuarioBanco == null)
            {
                throw new System.Exception("Houve um erro ao deletar"); //msotrará uma mensagem de exceção no codigo para o erro

            }
            else
            {
                // Remove o contato do banco de dados
                _bancoContext.Usuarios.Remove(UsuarioBanco);

                TempData["Mensagemsucesso"] = "Usuario apagado com sucesso";
                // Salva as alterações no banco
                _bancoContext.SaveChanges();


                // Busca a lista atualizada de contatos
                List<UsuarioModel> listaUsuarios = _bancoContext.Usuarios.ToList();

                // Redireciona para a action que exibe a listagem
                return RedirectToAction("ListagemUsuario");
            }
        }

        [HttpGet]
        public IActionResult EditarUsuarioView(int id)
        {
            // Buscar o contato pelo ID no banco de dados
            UsuarioModel? usuarioASerEditado = _bancoContext.Usuarios
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (usuarioASerEditado == null) throw new System.Exception("Esse id não existe no banco," +
                "tente novamente");
            // Se não encontrar o contato, exibe uma página de erro


            // Mapeando o contatoAntigo da ContatoModel para ContatoViewModel
            UsuarioViewModel UsuarioAntigo = new UsuarioViewModel
            {
                Id = usuarioASerEditado.Id,
                Nome = usuarioASerEditado.Nome,
                Login = usuarioASerEditado.Login,
                Email = usuarioASerEditado.Email,
                Perfil = usuarioASerEditado.Perfil,
                Senha = "XXXXXXXXXXXX",
                DataCadastro = usuarioASerEditado.DataCadastro,
            };

            return View(UsuarioAntigo);  // Passar o contato antigo para a EditarCtt
        }
        [HttpPost]
        public IActionResult EditarUsuarioView(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                // Buscar o contato pelo ID no banco de dados
                UsuarioModel? usuarioBanco = _bancoContext.Usuarios
                    .Where(w => w.Id == usuarioViewModel.Id)
                    .FirstOrDefault();

                if (usuarioBanco != null)
                {

                    // Criei um objeto para mapear os dados que vieram por parametro da usuarioViewModel para a ContatoModel
                    usuarioBanco.Nome = usuarioViewModel.Nome;
                    usuarioBanco.Email = usuarioViewModel.Email;
                    usuarioBanco.Perfil = usuarioViewModel.Perfil ?? 0;
                    usuarioBanco.DataAtualizacao = DateTime.Now;

                    // Salvar no banco
                    _bancoContext.SaveChanges();

                    return RedirectToAction("ListagemUsuario");  // Redireciona para a lista de contatos
                }
                else ModelState.AddModelError("", "Usuário inválido");
            }

            // Se o modelo não for válido, retorna à mesma view com os dados e erros de validação
            return View("EditarUsuarioView", usuarioViewModel);
        }

        // Métodos que não têm tipo são GET (apenas para buscar as informações da tela)
        // Métodos POST são usados para inclusão, ou seja, recebem a informação e cadastram

        // Método POST para criar um novo contato

        [HttpPost] // nao necessariamente precisa definir a rota em blazor
        public IActionResult CriarCtt(ContatoViewModel contato)
        {
            /* casting é quando tenho um tipo de dado e transformo em outro*/


            // verifica todas as condições (nulo, vazio e espaços em branco).
            if (!string.IsNullOrWhiteSpace(contato.Email))
            {
                ContatoModel? existing = _bancoContext.Contatos
                    .Where(w => w.Email.ToLower() == contato.Email.ToLower())
                    .FirstOrDefault();

                /* w representa cada linha da tabela Contatos no banco de dados.

                 Criei a variável existing para armazenar o resultado da consulta.

                 Se existir uma linha onde o e-mail gravado é igual ao e-mail do objeto contato 
                 (criado agora), guarda o primeiro registro encontrado. 
                 Caso não haja emails iguais, guarda null.  */
                // Se um email já existir, adiciona um erro ao ModelState
                if (existing != null)
                {
                    ModelState.AddModelError("Email", "Esse e-mail já existe!");
                }
            }
            try
            {
                if (ModelState.IsValid)
                {
                    /*Pegando os dados preenchidos da ContatoViewModel e passando para
                     ContatoModel de forma mais segura
                    */
                    ContatoModel contatosseguros = new ContatoModel
                    {
                        Nome = contato.Nome,
                        Email = contato.Email,
                        Celular = contato.Celular
                    };

                    // Adiciona o ContatoModel ao contexto do banco
                    _bancoContext.Add(contatosseguros);
                    TempData["Mensagemsucesso"] = "Contato cadastrado com sucesso";
                    // Salva as mudanças no banco de dados
                    _bancoContext.SaveChanges();

                    // Redireciona para a listagem
                    return RedirectToAction("Listagem");
                }
                //antes de passar o objeto, eu passo qual é o nome da view que ele vai cair. Forçando sem precisar fazer outra view
                return View("CriarCttView", contato);

            }
            catch (Exception erro)
            {
                TempData["Mensagemerro"] = $"Erro ao cadastrar contato,{erro.Message}";
                // Redireciona para a listagem
                return RedirectToAction("Listagem");
            }
        }

    }
}
