using System.ComponentModel.DataAnnotations;
namespace MEUPROJETO.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
    }
}
