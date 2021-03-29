using gerenciamento_usuario.Models;
using System.Web.Http;

namespace gerenciamento_usuario.Controllers.api
{
    public class ApiCadastroController : ApiController
    {
        [HttpPost]
        [ActionName("cadastrar-usuario")]
        public Resultado CadastrarUsuario(Usuario usuario)
        {
            if (!UtilsController.ValidaCPF(usuario.Cpf))
            {
                return new Resultado
                {
                    CodResultado = -1,
                    DescricaoResultado = "CPF Inválido"
                };
            }
            else if (!UtilsController.validaEmail(usuario.Email))
            {
                return new Resultado
                {
                    CodResultado = -2,
                    DescricaoResultado = "E-mail Inválido"
                };
            }
            else
            {
                return new Resultado
                {
                    CodResultado = 200,
                    DescricaoResultado = "OK"
                };
            }
        }

    }
}