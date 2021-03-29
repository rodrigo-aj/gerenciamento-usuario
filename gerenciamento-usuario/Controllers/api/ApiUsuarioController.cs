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
                        CodResultado = -1,
                        DescricaoResultado = "E-mail Inválido"
                    };
                }
                else
                {
                    UsuarioDataLayer userDL = new UsuarioDataLayer();

                    usuario.Cpf = UtilsController.RemoverCaracteresEspeciais(usuario.Cpf);

                    return userDL.InserirUsuario(usuario);
                }
            }
            catch (Exception ex)
            {
                return new Resultado
                {
                    CodResultado = -1,
                    DescricaoResultado = "Não foi possível inserir registro. Erro: " + ex.Message
                };
            }
        }

        [HttpGet]
        [ActionName("pesquisar-usuario")]
        public Usuario PesquisarUsuario(String cpf)
        {
            try
            {

                if (!UtilsController.ValidaCPF(cpf))
                {
                    return new Usuario
                    {
                        Id = -1,
                        Nome = "CPF Inválido"
                    };
                }
                else
                {
                    UsuarioDataLayer userDL = new UsuarioDataLayer();

                    cpf = UtilsController.RemoverCaracteresEspeciais(cpf.ToString());

                    var usuarioPesquiado = userDL.PesquisaUsuarioByCpf(cpf);

                    if (usuarioPesquiado == null)
                    {
                        return new Usuario
                        {
                            Id = -1,
                            Nome = "Usuário não encontrado!"
                        };
                    }
                    else
                    {
                        return usuarioPesquiado;
                    }


                }
            }
            catch (Exception ex)
            {
                return new Usuario
                {
                    Id = -1,
                    Nome = "Não foi possível realizar pesquisa. Erro: " + ex.Message
                };
            }
        }
        
    }
}