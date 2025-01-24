using System.ComponentModel.DataAnnotations;


namespace MEUPROJETO.ViewModels
{
        public class ContatoViewModel
        {

            public int Id { get; set; }

            [Required(ErrorMessage = "O nome é obrigatório")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O Email é obrigatório")]
            [EmailAddress(ErrorMessage = "Formato inválido")]
            public string Email { get; set; }

             [Required(ErrorMessage = "O Celular é obrigatório")]
             [Phone(ErrorMessage = "Formato inválido")]
            public string Celular { get; set; }
            }
    }

