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
        // string de conexão
        string connectionString = "server=localhost;user=root;password=root;database=db_empresa_one;";

        public async Task<bool> SalvarCadastro(Cliente novocliente)
        {
            try
            {
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
                await using var conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();

                string sql = "SELECT * FROM tb_cliente";

                await using var cmd = new MySqlCommand(sql, conn);

                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var cliente = new Cliente()
                    {
                        id = reader.GetInt32(0),
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

        public async Task ExcluirCliente(int id)
        {
            try
            {
                await using var conn = new MySqlConnection(connectionString);

                await conn.OpenAsync();

                string sql = "Delete FROM tb_cliente WHERE id= @id";

                await using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                await cmd.ExecuteNonQueryAsync();
            }

            catch
            {

            }

        }

        public async Task<Cliente?> BuscarClientePorID(int id)
        {
            try
            {
                await using var conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();

                string sql = "SELECT * FROM tb_cliente WHERE id = @id";

                await using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                await using var reader = await cmd.ExecuteReaderAsync();
                await reader.ReadAsync();

                Cliente cliente = new Cliente()
                {
                    id = reader.GetInt32(0),
                    nome = reader.GetString(1),
                    cpf = reader.GetString(2),
                    telefone = reader.GetString(3),
                };

                return cliente;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            try
            {
                await using var conn = new MySqlConnection(connectionString);

                await conn.OpenAsync();

                string sql = "UPDATE tb_cliente SET nome=@nome, cpf=@cpf, telefone=@telefone WHERE id = @id";

                await using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", cliente.id);
                cmd.Parameters.AddWithValue("@nome", cliente.nome);
                cmd.Parameters.AddWithValue("@cpf", cliente.cpf);
                cmd.Parameters.AddWithValue("@telefone", cliente.telefone);

                await cmd.ExecuteNonQueryAsync();

                //return rows > 0;

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
