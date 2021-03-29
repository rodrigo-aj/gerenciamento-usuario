using gerenciamento_usuario.Models;
using gerenciamento_usuario.Models.Cadastro;
using System;
using System.Web.Http;

namespace gerenciamento_usuario.Controllers.api
{
    public class ApiUsuarioController : ApiController
    {
        [HttpPost]
        [ActionName("cadastrar-usuario")]
        public Resultado CadastrarUsuario(Usuario usuario)
        {

            try
            {
                if (!UtilsController.ValidaCPF(usuario.Cpf))
                {
                    return new Resultado
                    {
                        CodResultado = -1,
                        DescricaoResultado = "CPF Inválido"
                    };
                }
                else if (!UtilsController.ValidaEmail(usuario.Email))
                {
                    return new Resultado
                    {
                        CodResultado = -2,
                        DescricaoResultado = "E-mail Inválido"
                    };
                }
                else
                {
                    UsuarioDataLayer cadDL = new UsuarioDataLayer();

                    usuario.Cpf = UtilsController.RemoverCaracteresEspeciais(usuario.Cpf);

                    return cadDL.InserirUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return new Resultado
                {
                    CodResultado = -3,
                    DescricaoResultado = "Não foi possível inserir registro. Erro: " + ex.Message
                };
            }
        }

    }
}