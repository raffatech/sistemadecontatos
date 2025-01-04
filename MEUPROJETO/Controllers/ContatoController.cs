using MEUPROJETO.Models;
using MEUPROJETO.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace MEUPROJETO.Controllers
{
    [Route("Contato")] // Rota fixa para a controller Contato
    [ApiController] // Definindo que é uma controladora de API
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        // Quem insere os registros é o ContatoRepositorio, criei construtor para injetá-lo
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        // Método para exibir a listagem de contatos
        [HttpGet("Listagem")]
        public IActionResult Listagem()
        {
            return View();
        }

        // Método para exibir a tela de criação de um novo contato
        [HttpGet("CriarCttView")]
        public IActionResult CriarCttView()
        {
            return View();
        }

        // Método para exibir a tela de edição de um contato
        [HttpGet("Editar")]
        public IActionResult EditarCtt()
        {
            return View();
        }

        // Método para exibir a tela de confirmação de exclusão de um contato
        [HttpGet("Apagar")]
        public IActionResult ApagarCttConfirmacao()
        {
            return View();
        }

        // Métodos que não têm tipo são GET (apenas para buscar as informações da tela)
        // Métodos POST são usados para inclusão, ou seja, recebem a informação e cadastram

        // Método POST para criar um novo contato
        [HttpPost("Criar")]
        public IActionResult CriarCtt([FromBody] ContatoModel contato)
        {
            _contatoRepositorio.Adicionar(contato);

            return RedirectToAction("Index");
        }

        /*
        [HttpGet("id")]
         public IActionResult ConsultarCtt()
        {

        }
       */


    }
}
