using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interface;
using ConsoleApp1.Database;
using ConsoleApp1.Models;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MySql.Data.MySqlClient;

namespace ConsoleApp1.Models
{
    class PessoaDAO : IDAO<Pessoa>
    {
        private static Conexao conn;


        public PessoaDAO()
        {
            conn = new Conexao();
        }
        public bool Delete(Pessoa t)
        {
            try
            {
                var queryPessoa = conn.Query();
                queryPessoa.CommandText = "DELETE FROM Pessoa WHERE idPessoa = @id";
                queryPessoa.Parameters.AddWithValue("@id", t.IdPessoa);

                var resultPessoa = queryPessoa.ExecuteNonQuery();

                var queryEndereco = conn.Query();
                queryEndereco.CommandText = "DELETE FROM Endereco WHERE idEndereco = @id";
                queryEndereco.Parameters.AddWithValue("@id", t.endereco.IdEnd);

                var resultEndereco = queryEndereco.ExecuteNonQuery();

                var queryTelefone = conn.Query();
                queryTelefone.CommandText = "DELETE FROM Telefone WHERE idTelefone = @id";
                queryTelefone.Parameters.AddWithValue("@id", t.telefones[0].IdTel);

                var resultTelefone = queryTelefone.ExecuteNonQuery();

                var queryTipoTelefone = conn.Query();
                queryTipoTelefone.CommandText = "DELETE FROM Telefone_Tipo WHERE idTelefone_Tipo = @id";
                queryTipoTelefone.Parameters.AddWithValue("@id", t.telefones[0].TipoTel.IdTipo);

                var resultTipoTelefone = queryTipoTelefone.ExecuteNonQuery();

                if (resultPessoa == 0)
                    throw new Exception("Registro não removido");
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }
        public bool Insert(Pessoa t)
        {
            try
            {
                var queryEndereco = conn.Query();
                queryEndereco.CommandText = "INSERT INTO Endereco (Logradouro, Numero, CEP, Bairro, Cidade, Estado)" +
                                    "VALUES (@Logradouro, @Numero, @CEP, @Bairro, @Cidade, @Estado)";
                queryEndereco.Parameters.AddWithValue("@Logradouro", t.endereco.Logradouro);
                queryEndereco.Parameters.AddWithValue("@Numero", t.endereco.NumeroEnd);
                queryEndereco.Parameters.AddWithValue("@CEP", t.endereco.Cep);
                queryEndereco.Parameters.AddWithValue("@Bairro", t.endereco.Bairro);
                queryEndereco.Parameters.AddWithValue("@Cidade", t.endereco.Cidade);
                queryEndereco.Parameters.AddWithValue("@Estado", t.endereco.Estado);

                var resultEndereco = queryEndereco.ExecuteNonQuery();
                var resultIdEndereco = queryEndereco.LastInsertedId;
                

                var queryTipoTelefone = conn.Query();
                queryTipoTelefone.CommandText = "INSERT INTO Telefone_Tipo (Tipo)" +
                                    "VALUES (@Tipo)";             
                queryTipoTelefone.Parameters.AddWithValue("@Tipo", t.telefones[0].TipoTel.Tipo) ;

                var resultTipoTelefone = queryTipoTelefone.ExecuteNonQuery();
                var resultIdTipoTelefone = queryTipoTelefone.LastInsertedId;
                
            
                var queryTelefone = conn.Query();
                queryTelefone.CommandText = "INSERT INTO Telefone (idTelefone_Tipo, Numero, DDD)" +
                                    "VALUES (@idTelefone_Tipo, @Numero, @DDD)";
                queryTelefone.Parameters.AddWithValue("@idTelefone_tipo", resultIdTipoTelefone);
                queryTelefone.Parameters.AddWithValue("@Numero", t.telefones[0].NumeroTel);
                queryTelefone.Parameters.AddWithValue("@DDD", t.telefones[0].Ddd);
                
                var resultTelefone = queryTelefone.ExecuteNonQuery();
                var resultIdTelefone = queryTelefone.LastInsertedId;
                

                var queryPessoa = conn.Query();
                queryPessoa.CommandText = "INSERT INTO Pessoa (idEndereco, Nome, CPF)" +
                                    "VALUES (@idEndereco, @Nome, @CPF)";
                queryPessoa.Parameters.AddWithValue("@idEndereco", resultIdEndereco);
                queryPessoa.Parameters.AddWithValue("@Nome", t.Nome);
                queryPessoa.Parameters.AddWithValue("@CPF", t.Cpf);
               
                var resutPessoa = queryPessoa.ExecuteNonQuery();
                var resultIdPessoa = queryPessoa.LastInsertedId;


               var queryPessoa_Telefone = conn.Query();
               queryPessoa_Telefone.CommandText = "INSERT INTO Pessoa_Telefone(idPessoa, idTelefone)" +
                                    "VALUES (@idPessoa, @idTelefone)";
                queryPessoa_Telefone.Parameters.AddWithValue("@idPessoa", resultIdPessoa);
                queryPessoa_Telefone.Parameters.AddWithValue("@idTelefone", resultTelefone);

               var resultPessoa_Telefone = queryPessoa_Telefone.ExecuteReader();

            } catch (Exception e)
            {
                throw e;
            }
            finally {
                conn.Close();

            }
            return false;
        }

        public Pessoa List(long cpf)
        {
            try
            {
                var queryPessoa = conn.Query();
                queryPessoa.CommandText = "SELECT * FROM Pessoa INNER JOIN Endereco ON Pessoa.idIndereco = Endereco.idIndereco " +
                                         "INNER JOIN Pessoa_Telefone ON Pessoa.idPessoa = Pessoa_Telefone.idPessoa " +
                                         "INNER JOIN Telefone ON Telefone.idTelefone = Pessoa_Telefone.idTelefone " +
                                         "INNER JOIN Telefone_Tipo ON Telefone_Tipo.idTelefone_Tipo = Telefone.idTelefone_Tipo " +
                "WHERE CPF = @CPF";
                queryPessoa.Parameters.AddWithValue("@CPF", cpf.ToString());
                MySqlDataReader reader = queryPessoa.ExecuteReader();

                if (!reader.HasRows)
                    throw new Exception("Nenhum registro foi encontrado!");
                var pessoa = new Pessoa();
                while(reader.Read())
                {

                    pessoa.IdPessoa = reader.GetInt32("idPessoa");
                    pessoa.Nome = reader.GetString("Nome");
                    pessoa.Cpf = reader.GetString("CPF");
                    pessoa.endereco.IdEnd = reader.GetInt32("idEndereco");
                    pessoa.endereco.Logradouro = reader.GetString("Logradouro");
                    pessoa.endereco.NumeroEnd = reader.GetInt32("Numero");
                    pessoa.endereco.Cep = reader.GetInt32("CEP");
                    pessoa.endereco.Bairro = reader.GetString("Bairro");
                    pessoa.endereco.Cidade = reader.GetString("Cidade");
                    pessoa.endereco.Estado = reader.GetString("Estado");
                    pessoa.telefones[0].IdTel = reader.GetInt32("idTelefone");
                    pessoa.telefones[0].NumeroTel = reader.GetInt32("Numero");
                    pessoa.telefones[0].Ddd = reader.GetInt32("DDD");
                    pessoa.telefones[0].TipoTel.IdTipo = reader.GetInt32("idTelefone_Tipo");
                    pessoa.telefones[0].TipoTel.Tipo = reader.GetString("Tipo");


                }
                return pessoa;
            }
            catch (Exception e) { throw e; }
        }
        public bool Update(Pessoa t)
        {
            try
            {
                var queryEndereco = conn.Query();
                queryEndereco.CommandText = "UPDATE Endereco SET Logradouro = @Logradouro, Numero = @Numero, CEP = @CEP, " +
                                            "Cidade = @Cidade, Estado = @Estado" +
                    "WHERE idEndereco = @idEndereco";
                queryEndereco.Parameters.AddWithValue("@idEndereco", t.endereco.IdEnd);
                queryEndereco.Parameters.AddWithValue("@Logradouro", t.endereco.Logradouro);
                queryEndereco.Parameters.AddWithValue("@Numero", t.endereco.NumeroEnd);
                queryEndereco.Parameters.AddWithValue("@CEP", t.endereco.Cep);
                queryEndereco.Parameters.AddWithValue("@Bairro", t.endereco.Bairro);
                queryEndereco.Parameters.AddWithValue("@Cidade", t.endereco.Cidade);
                queryEndereco.Parameters.AddWithValue("@Estado", t.endereco.Estado);

                var resultEndereco = queryEndereco.ExecuteNonQuery();


                var queryTipoTelefone = conn.Query();
                queryTipoTelefone.CommandText = "UPDATE Telefone_Tipo SET Tipo = @Tipo" +
                     "WHERE idTipo = @idTipo ";
                queryTipoTelefone.Parameters.AddWithValue("@idTipo", t.telefones[0].IdTel);
                queryTipoTelefone.Parameters.AddWithValue("@Tipo", t.telefones[0].TipoTel);

                var resultTipoTelefone = queryTipoTelefone.ExecuteNonQuery();


                var queryTelefone = conn.Query();
                queryTelefone.CommandText = "UPDATE Telefone SET Numero = @Numero, DDD = @DDD" +
                     "WHERE idTelefone = @idTelefone";
                queryTelefone.Parameters.AddWithValue("@idTelefone", t.telefones[0].IdTel);
                queryTelefone.Parameters.AddWithValue("@Numero", t.telefones[0].NumeroTel);
                queryTelefone.Parameters.AddWithValue("@DDD", t.telefones[0].Ddd);

                var resultTelefone = queryTelefone.ExecuteNonQuery();


                var queryPessoa = conn.Query();
                queryPessoa.CommandText = "UPDATE Pessoa SET Nome = @Nome, CPF = @CPF" +
                      "WHERE idPessoa = @idPessoa";
                queryPessoa.Parameters.AddWithValue("@idPessoa", t.IdPessoa);
                queryPessoa.Parameters.AddWithValue("@Nome", t.Nome);
                queryPessoa.Parameters.AddWithValue("@CPF", t.Cpf);

                var resultPessoa = queryPessoa.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();

            }
            return false;
        }
    }
}
