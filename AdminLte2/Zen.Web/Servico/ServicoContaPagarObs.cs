using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoContaPagarObs
    {
        public ContaPagarObs ObterObjetoPorId(int id)
        {
            string sqlString = "SELECT * FROM contaspagarobs " +
                    "where IDTITULO = " + id.ToString();
            var conexao = Conectar();
            var consulta = conexao.CreateCommand();
            consulta.CommandText = sqlString;
            var registros = consulta.ExecuteReader();

            var lst = new List<ContaPagarObs>();

            while (registros.Read())
            {
                lst.Add(new ContaPagarObs()
                {
                    IdTitulo = Convert.ToInt32(registros["IDTITULO"]),
                    Marca = (string)registros["MARCA"],
                    Produto = (string)registros["PRODUTO"],
                    Quantidade = (string)registros["QUANT"],
                    Valor = (string)registros["VALOR"]
                });
            }
            //close connection
            Desconectar(conexao);

            return lst.FirstOrDefault();
        }

        private MySqlConnection Conectar()
        {
            var stringConexao = "Database=Zen; Data Source=localhost;User Id=root;Password=12345678";
            MySqlConnection conexao;
            try
            {
                conexao = new MySqlConnection(stringConexao);
                conexao.Open();
                return conexao;
            }
            catch
            {
                return null;
            }
        }

        public void Desconectar(MySqlConnection conn)
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        private void Insert(ContaPagarObs objeto)
        {
            string query = $@"INSERT INTO contaspagarobs (IDTITULO, PRODUTO, QUANT, MARCA, VALOR) VALUES(" + objeto.IdTitulo.ToString() + ",'" + objeto.Produto.ToString() + "','" + objeto.Quantidade.ToString() + "','" + objeto.Marca.ToString() + "','" + objeto.Valor.ToString() + "')";
            var conexao = Conectar();

            //open connection
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                //create command and assign the query and connection from the constructor
                var cmd = new MySqlCommand(query, conexao);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Desconectar(conexao);
            }
        }

        public void Salvar(ContaPagarObs objeto)
        {
            var contapagarobs = ObterObjetoPorId(objeto.IdTitulo);
            if(contapagarobs == null)
            {
                Insert(objeto);
            }
            else
            {
                Update(objeto);
            }
        }

        //Update statement
        private void Update(ContaPagarObs objeto)
        {
            string query = $@"UPDATE contaspagarobs SET PRODUTO=@PRODUTO, QUANT=@QUANT, MARCA = @MARCA, VALOR = @VALOR 
                            WHERE IDTITULO=@IDTITULO";
            var conexao = Conectar();
            //Open connection
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                var cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@IDTITULO", objeto.IdTitulo);
                cmd.Parameters.AddWithValue("@PRODUTO", objeto.Produto);
                cmd.Parameters.AddWithValue("@QUANT", objeto.Quantidade);
                cmd.Parameters.AddWithValue("@MARCA", objeto.Marca);
                cmd.Parameters.AddWithValue("@VALOR", objeto.Valor);
                
                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Desconectar(conexao);
            }
        }

        public void Delete(ContaPagarObs objeto)
        {
            string query = $@"delete from contaspagarobs WHERE IDTITULO=@IDTITULO";
            var conexao = Conectar();
            //Open connection
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                var cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@IDTITULO", objeto.IdTitulo);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Desconectar(conexao);
            }
        }
    }
}