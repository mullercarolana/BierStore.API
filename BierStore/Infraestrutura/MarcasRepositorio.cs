using BierStore.Models;
using BierStore.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BierStore.Infraestrutura
{
    public sealed class MarcasRepositorio : IMarcasRepositorio
    {
        private readonly string _stringConexao;

        public MarcasRepositorio(string stringConexao)
        {
            _stringConexao = stringConexao;
        }

        public async Task<Marca> IncluirMarcaAsync(Marca marca)
        {
            const string sql = @"INSERT INTO dbo.Marcas (Nome, DataInclusao, DataUltimaAlteracao) 
                VALUES ( @Nome, @DataInclusao, @DataUltimaAlteracao);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var id = await conexao.QuerySingleAsync<int>(sql, new
                    {
                        marca.Nome,
                        marca.DataInclusao,
                        marca.DataUltimaAlteracao
                    });

                    if (id == 0)
                    {
                        throw new InvalidOperationException("Falha ao incluir a marca");
                    }

                    marca.NovoId(id);
                    return marca;
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao incluir marca.", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<bool> AtualizarMarcaAsync(int id, Marca marca)
        {
            const string sql = @"UPDATE dbo.Marcas
                                 SET Nome = @Nome,
                                     DataUltimaAlteracao = @DataUltimaAlteracao
                                 WHERE Id = @id";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var linhasAfetadas = await conexao.ExecuteAsync(sql, new
                    {
                        id,
                        marca.Nome,
                        marca.DataUltimaAlteracao
                    });

                    if (linhasAfetadas <= 0) throw new InvalidOperationException("Não foi possível salvar a atualização da marca");

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao atualizar marca.", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<Marca> RecuperarMarcaPorIdAsync(int id)
        {
            const string sql = @"SELECT Id, Nome, DataInclusao, DataUltimaAlteracao FROM dbo.Marcas WHERE Id=@id AND Excluido=0;";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var marcaRecuperada = await conexao.QueryFirstOrDefaultAsync<Marca>(sql, new { id });

                    return marcaRecuperada;
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao recuperar a marca por id", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<IEnumerable<Marca>> RecuperarTodasMarcasAsync()
        {
            const string sql = @"SELECT Id, Nome, DataInclusao, DataUltimaAlteracao FROM dbo.Marcas WHERE Excluido=0;";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var recuperarMarcas = await conexao.QueryAsync<Marca>(sql);

                    return recuperarMarcas.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao recuperar todas as marcas", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<IEnumerable<RecuperarCervejaPorMarcaViewModel>> RecuperarTodasAsCervejasPorMarcaAsync(int id)
        {
            const string sql = @"	SELECT  Marcas.Id, 
			                                Marcas.Nome, 
			                                Cervejas.Nome AS TipoDaCerveja, 
			                                Cervejas.Origem,
                                            Cervejas.CategoriaAlcoolica,
			                                Cervejas.Preco
	                                FROM dbo.Marcas 
	                                INNER JOIN dbo.Cervejas ON Cervejas.IdMarca = Marcas.Id
	                                WHERE Marcas.Excluido = 0 AND Marcas.Id = @id;";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var recuperarMarcas = await conexao.QueryAsync<RecuperarCervejaPorMarcaViewModel>(sql, new { id });

                    return recuperarMarcas.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao recuperar todas as cervejas por marca", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<bool> RemoverMarcaAsync(int id)
        {
            const string sql = @"UPDATE dbo.Marcas
                                 SET Excluido = 1
                                 WHERE Id = @id";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var resultado = await conexao.ExecuteAsync(sql, new { id });

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao remover a marca.", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
