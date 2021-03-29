using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gerenciamento_usuario.Models.Cadastro
{
    public class UsuarioDataLayer
    {
        private string conn = ConfigurationManager.ConnectionStrings["mysql_connection"].ToString();

        public Resultado InserirUsuario(Usuario usuario)
        {
            using (IDbConnection db = new MySqlConnection(conn))
            {
                db.Open();

                try
                {
                    if (this.PesquisaUsuarioByCpf(usuario.Cpf) != null)
                    {
                        return new Resultado
                        {
                            CodResultado = -1,
                            DescricaoResultado = "CPF já cadastrado!"
                        };
                    }

                    var p = new DynamicParameters();

                    p.Add("@nome", usuario.Nome, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@cpf", usuario.Cpf, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@email", usuario.Email, dbType: DbType.String, direction: ParameterDirection.Input);

                    var resultado = db.Query(@" INSERT INTO usuario
                                                (
	                                                nome
                                                ,	cpf
                                                ,	email
                                                )
                                                VALUES
                                                (
	                                                @nome
                                                ,	@cpf
                                                ,	@email)", p, commandType: CommandType.Text);

                    return new Resultado
                    {
                        CodResultado = 200,
                        DescricaoResultado = "OK"
                    };

                }
                catch (Exception ex)
                {
                    return new Resultado
                    {
                        CodResultado = -1,
                        DescricaoResultado = "Não foi possível inserir registro. Erro: " + ex.Message
                    };
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                    {
                        db.Close();
                    }
                }
            }
        }

        public Usuario PesquisaUsuarioByCpf(string cpf)
        {
            using (IDbConnection db = new MySqlConnection(conn))
            {
                db.Open();

                try
                {
                    var p = new DynamicParameters();

                    p.Add("@cpf", cpf, dbType: DbType.String, direction: ParameterDirection.Input);

                    return db.Query<Usuario>(@"select id, nome, cpf, email from usuario u where u.cpf = @cpf", p, commandType: CommandType.Text).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    return new Usuario
                    {
                        Id = -1,
                        Nome = "Não foi possível realizar busca. Erro: " + ex.Message
                    };
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                    {
                        db.Close();
                    }
                }
            }
        }
    }
}