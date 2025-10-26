using Microsoft.Data.SqlClient;
using PDVnet.GestaoProdutos.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        public List<Produto> ObterTodos()
        {
            var produtos = new List<Produto>();

            using (var connection = ConnectionHelper.CreateConnection())
            {
                connection.Open();

                var query = "SELECT Id, Nome, Descricao, Preco, Quantidade, DataCadastro, DataAtualizacao FROM Produtos";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var idxId = reader.GetOrdinal("Id");
                        var idxNome = reader.GetOrdinal("Nome");
                        var idxDescricao = reader.GetOrdinal("Descricao");
                        var idxPreco = reader.GetOrdinal("Preco");
                        var idxQuantidade = reader.GetOrdinal("Quantidade");
                        var idxDataCadastro = reader.GetOrdinal("DataCadastro");
                        var idxDataAtualizacao = reader.GetOrdinal("DataAtualizacao");

                        produtos.Add(new Produto
                        {
                            Id = reader.GetInt32(idxId),
                            Nome = reader.GetString(idxNome),
                            Descricao = reader.IsDBNull(idxDescricao) ? null : reader.GetString(idxDescricao),
                            Preco = reader.GetDecimal(idxPreco),
                            Quantidade = reader.GetDecimal(idxQuantidade),
                            DataCadastro = reader.GetDateTime(idxDataCadastro),
                            DataAtualizacao = reader.IsDBNull(idxDataAtualizacao) ? null : reader.GetDateTime(idxDataAtualizacao)
                        });
                    }
                }
            }

            return produtos;
        }

        public async Task<List<Produto>> ObterTodosAsync()
        {
            var produtos = new List<Produto>();

            using (var connection = ConnectionHelper.CreateConnection())
            {
                await connection.OpenAsync();

                var query = "SELECT Id, Nome, Descricao, Preco, Quantidade, DataCadastro, DataAtualizacao FROM Produtos";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var idxId = reader.GetOrdinal("Id");
                        var idxNome = reader.GetOrdinal("Nome");
                        var idxDescricao = reader.GetOrdinal("Descricao");
                        var idxPreco = reader.GetOrdinal("Preco");
                        var idxQuantidade = reader.GetOrdinal("Quantidade");
                        var idxDataCadastro = reader.GetOrdinal("DataCadastro");
                        var idxDataAtualizacao = reader.GetOrdinal("DataAtualizacao");

                        produtos.Add(new Produto
                        {
                            Id = reader.GetInt32(idxId),
                            Nome = reader.GetString(idxNome),
                            Descricao = reader.IsDBNull(idxDescricao) ? null : reader.GetString(idxDescricao),
                            Preco = reader.GetDecimal(idxPreco),
                            Quantidade = reader.GetDecimal(idxQuantidade),
                            DataCadastro = reader.GetDateTime(idxDataCadastro),
                            DataAtualizacao = reader.IsDBNull(idxDataAtualizacao) ? null : reader.GetDateTime(idxDataAtualizacao)
                        });
                    }
                }
            }

            return produtos;
        }

        public void Adicionar(Produto produto)
        {
            using (var connection = ConnectionHelper.CreateConnection())
            {
                connection.Open();

                var query = @"INSERT INTO Produtos (Nome, Descricao, Preco, Quantidade) 
                              VALUES (@Nome, @Descricao, @Preco, @Quantidade)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = (object)produto.Nome ?? DBNull.Value;
                    command.Parameters.Add("@Descricao", SqlDbType.NVarChar, 255).Value = (object)produto.Descricao ?? DBNull.Value;

                    var precoParam = command.Parameters.Add("@Preco", SqlDbType.Decimal);
                    precoParam.Precision = 10;
                    precoParam.Scale = 2;
                    precoParam.Value = produto.Preco;

                    var quantidadeParam = command.Parameters.Add("@Quantidade", SqlDbType.Decimal);
                    quantidadeParam.Precision = 10;
                    quantidadeParam.Scale = 2;
                    quantidadeParam.Value = produto.Quantidade;

                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task AdicionarAsync(Produto produto)
        {
            using (var connection = ConnectionHelper.CreateConnection())
            {
                await connection.OpenAsync();

                var query = @"INSERT INTO Produtos (Nome, Descricao, Preco, Quantidade) 
                              VALUES (@Nome, @Descricao, @Preco, @Quantidade)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = (object)produto.Nome ?? DBNull.Value;
                    command.Parameters.Add("@Descricao", SqlDbType.NVarChar, 255).Value = (object)produto.Descricao ?? DBNull.Value;

                    var precoParam = command.Parameters.Add("@Preco", SqlDbType.Decimal);
                    precoParam.Precision = 10;
                    precoParam.Scale = 2;
                    precoParam.Value = produto.Preco;

                    var quantidadeParam = command.Parameters.Add("@Quantidade", SqlDbType.Decimal);
                    quantidadeParam.Precision = 10;
                    quantidadeParam.Scale = 2;
                    quantidadeParam.Value = produto.Quantidade;

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public Produto? ObterPorId(int id)
        {
            using (var connection = ConnectionHelper.CreateConnection())
            {
                connection.Open();

                var query = "SELECT Id, Nome, Descricao, Preco, Quantidade, DataCadastro, DataAtualizacao FROM Produtos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var idxId = reader.GetOrdinal("Id");
                            var idxNome = reader.GetOrdinal("Nome");
                            var idxDescricao = reader.GetOrdinal("Descricao");
                            var idxPreco = reader.GetOrdinal("Preco");
                            var idxQuantidade = reader.GetOrdinal("Quantidade");
                            var idxDataCadastro = reader.GetOrdinal("DataCadastro");
                            var idxDataAtualizacao = reader.GetOrdinal("DataAtualizacao");

                            return new Produto
                            {
                                Id = reader.GetInt32(idxId),
                                Nome = reader.GetString(idxNome),
                                Descricao = reader.IsDBNull(idxDescricao) ? null : reader.GetString(idxDescricao),
                                Preco = reader.GetDecimal(idxPreco),
                                Quantidade = reader.GetDecimal(idxQuantidade),
                                DataCadastro = reader.GetDateTime(idxDataCadastro),
                                DataAtualizacao = reader.IsDBNull(idxDataAtualizacao) ? null : reader.GetDateTime(idxDataAtualizacao)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            using (var connection = ConnectionHelper.CreateConnection())
            {
                await connection.OpenAsync();

                var query = "SELECT Id, Nome, Descricao, Preco, Quantidade, DataCadastro, DataAtualizacao FROM Produtos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var idxId = reader.GetOrdinal("Id");
                            var idxNome = reader.GetOrdinal("Nome");
                            var idxDescricao = reader.GetOrdinal("Descricao");
                            var idxPreco = reader.GetOrdinal("Preco");
                            var idxQuantidade = reader.GetOrdinal("Quantidade");
                            var idxDataCadastro = reader.GetOrdinal("DataCadastro");
                            var idxDataAtualizacao = reader.GetOrdinal("DataAtualizacao");

                            return new Produto
                            {
                                Id = reader.GetInt32(idxId),
                                Nome = reader.GetString(idxNome),
                                Descricao = reader.IsDBNull(idxDescricao) ? null : reader.GetString(idxDescricao),
                                Preco = reader.GetDecimal(idxPreco),
                                Quantidade = reader.GetDecimal(idxQuantidade),
                                DataCadastro = reader.GetDateTime(idxDataCadastro),
                                DataAtualizacao = reader.IsDBNull(idxDataAtualizacao) ? null : reader.GetDateTime(idxDataAtualizacao)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void Atualizar(Produto produto)
        {
            using (var connection = ConnectionHelper.CreateConnection())
            {
                connection.Open();

                var query = @"UPDATE Produtos 
                              SET Nome = @Nome, Descricao = @Descricao, 
                                  Preco = @Preco, Quantidade = @Quantidade, DataAtualizacao = GETDATE() 
                              WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = produto.Id;
                    command.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = (object)produto.Nome ?? DBNull.Value;
                    command.Parameters.Add("@Descricao", SqlDbType.NVarChar, 255).Value = (object)produto.Descricao ?? DBNull.Value;

                    var precoParam = command.Parameters.Add("@Preco", SqlDbType.Decimal);
                    precoParam.Precision = 10;
                    precoParam.Scale = 2;
                    precoParam.Value = produto.Preco;

                    var quantidadeParam = command.Parameters.Add("@Quantidade", SqlDbType.Decimal);
                    quantidadeParam.Precision = 10;
                    quantidadeParam.Scale = 2;
                    quantidadeParam.Value = produto.Quantidade;

                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task AtualizarAsync(Produto produto)
        {
            using (var connection = ConnectionHelper.CreateConnection())
            {
                await connection.OpenAsync();

                var query = @"UPDATE Produtos 
                              SET Nome = @Nome, Descricao = @Descricao, 
                                  Preco = @Preco, Quantidade = @Quantidade, DataAtualizacao = GETDATE() 
                              WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = produto.Id;
                    command.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = (object)produto.Nome ?? DBNull.Value;
                    command.Parameters.Add("@Descricao", SqlDbType.NVarChar, 255).Value = (object)produto.Descricao ?? DBNull.Value;

                    var precoParam = command.Parameters.Add("@Preco", SqlDbType.Decimal);
                    precoParam.Precision = 10;
                    precoParam.Scale = 2;
                    precoParam.Value = produto.Preco;

                    var quantidadeParam = command.Parameters.Add("@Quantidade", SqlDbType.Decimal);
                    quantidadeParam.Precision = 10;
                    quantidadeParam.Scale = 2;
                    quantidadeParam.Value = produto.Quantidade;

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public bool Excluir(int id)
        {
            using (var connection = ConnectionHelper.CreateConnection())
            {
                connection.Open();

                var query = "DELETE FROM Produtos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    int linhasAfetadas = command.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            using (var connection = ConnectionHelper.CreateConnection())
            {
                await connection.OpenAsync();

                var query = "DELETE FROM Produtos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    int linhasAfetadas = await command.ExecuteNonQueryAsync();
                    return linhasAfetadas > 0;
                }
            }
        }
    }
}