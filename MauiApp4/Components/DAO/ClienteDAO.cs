using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp4.Components.model;
using MySqlConnector;

namespace MauiApp4.Components.DAO
{
    public class ClienteDAO
    {
        public async Task<bool> SalvarCadastro(Cliente novocliente)
        {
            try
            {
                // string de conexão
                string connectionString = "server=localhost;user=root;password=root;database=db_empresa_one;";

                await using var conn = new MySqlConnection(connectionString);

                await conn.OpenAsync();

                string sql = "INSERT INTO tb_cliente (nome, cpf, telefone) VALUES(@nome, @cpf, @telefone)";
                
                await using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", novocliente.nome);
                cmd.Parameters.AddWithValue("@cpf", novocliente.cpf);
                cmd.Parameters.AddWithValue("@telefone", novocliente.telefone);

                int rows = await cmd.ExecuteNonQueryAsync();

                return rows > 0;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            var lista = new List<Cliente>();

            try
            {
                // string de conexão
                string connectionString = "server=localhost;user=root;password=root;database=db_empresa_one;";

                await using var conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();

                string sql = "SELECT * FROM tb_cliente";

                await using var cmd = new MySqlCommand( sql, conn);

                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var cliente = new Cliente()
                    {
                        nome = reader.GetString(1),
                        cpf = reader.GetString(2),
                        telefone = reader.GetString(3)
                    };


                    lista.Add(cliente);
                }

                return lista;
               
            }
            catch (Exception ex)
            {
               return new List<Cliente>();
            }
        }

    }
}
