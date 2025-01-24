
using MEUPROJETO.Data;
using MEUPROJETO.Models;
using Microsoft.AspNetCore.Mvc;

namespace MEUPROJETO.Controllers
{
    public class LoginController : Controller
    {
        private readonly BancoContext _bancoContext;

        // criei construtor para injetar o banco e inserir os registros direto
        public LoginController(BancoContext bancoContext)
        {

            _bancoContext = bancoContext;
        }

        public IActionResult LoginTela()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel modelo)
        {
            if(ModelState.IsValid)
            {
                UsuarioModel? usuario = _bancoContext.Usuarios
                    .Where(w => w.Login == modelo.Login && w.Senha == modelo.Senha)
                    .FirstOrDefault();
                if(usuario != null)
                {
                    // Autenticado
                    return RedirectToAction("Listagem", "Contato");
                }
            }

            TempData["MensagemErro"] = "Não foi possivel realizar seu login, tente novamente" ;
            return View("LoginTela");
        }
    }
}
