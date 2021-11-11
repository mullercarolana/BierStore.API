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
    public sealed class CervejasRepositorio : ICervejasRepositorio
    {
        private readonly string _stringConexao;

        public CervejasRepositorio(string stringConexao)
        {
            _stringConexao = stringConexao;
        }

        public async Task<Cerveja> IncluirCervejaAsync(Cerveja cerveja)
        {
            const string sql = @"INSERT INTO dbo.Cervejas (IdMarca, Nome, Origem, CategoriaAlcoolica, Preco, DataInclusao, DataUltimaAlteracao) 
                VALUES (@IdMarca, @Nome, @Origem, @CategoriaAlcoolica, @Preco, @DataInclusao, @DataUltimaAlteracao);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var id = await conexao.QuerySingleAsync<int>(sql, new
                    {
                        cerveja.IdMarca,
                        cerveja.Nome,
                        cerveja.Origem,
                        cerveja.Preco,
                        cerveja.CategoriaAlcoolica,
                        cerveja.DataInclusao,
                        cerveja.DataUltimaAlteracao
                    });

                    if (id == 0)
                    {
                        throw new InvalidOperationException("Falha ao incluir a cerveja");
                    }

                    cerveja.NovoId(id);
                    return cerveja;
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao incluir a cerveja.", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<bool> AtualizarCervejaAsync(int id, Cerveja cerveja)
        {
            const string sql = @"UPDATE dbo.Cervejas
                                 SET IdMarca = @IdMarca,
                                     Nome = @Nome,
                                     Origem = @Origem,
                                     CategoriaAlcoolica = @CategoriaAlcoolica,
                                     Preco = @Preco,
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
                        cerveja.IdMarca,
                        cerveja.Nome,
                        cerveja.Origem,
                        cerveja.CategoriaAlcoolica,
                        cerveja.Preco,
                        cerveja.DataUltimaAlteracao
                    });

                    if (linhasAfetadas <= 0) throw new InvalidOperationException("Não foi possível salvar a atualização da cerveja");

                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao atualizar a cerveja.", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<CervejaViewModel> RecuperarCervejaPorIdAsync(int id)
        {
            const string sql = @"SELECT Cervejas.Id, 
			                            Cervejas.IdMarca, 
			                            Marcas.Nome AS NomeDaMarca, 
			                            Cervejas.Nome AS TipoCerveja, 
			                            Cervejas.Origem, 
			                            Cervejas.CategoriaAlcoolica, 
			                            Cervejas.Preco, 
			                            Cervejas.DataInclusao, 
			                            Cervejas.DataUltimaAlteracao 
	                            FROM dbo.Cervejas (NOLOCK)
	                            INNER JOIN dbo.Marcas (NOLOCK) ON Marcas.Id = Cervejas.IdMarca
	                            WHERE Cervejas.Excluido = 0 AND Cervejas.Id = @id;";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var cervejaRecuperada = await conexao.QueryFirstOrDefaultAsync<CervejaViewModel>(sql, new { id });

                    return cervejaRecuperada;
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao recuperar a cerveja por Id", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<IEnumerable<CervejaViewModel>> RecuperarTodasCervejasAsync()
        {
            const string sql = @"SELECT Cervejas.Id, 
			                            Cervejas.IdMarca, 
			                            Marcas.Nome AS NomeDaMarca, 
			                            Cervejas.Nome AS TipoCerveja, 
			                            Cervejas.Origem, 
			                            Cervejas.CategoriaAlcoolica, 
			                            Cervejas.Preco, 
			                            Cervejas.DataInclusao, 
			                            Cervejas.DataUltimaAlteracao 
	                            FROM dbo.Cervejas (NOLOCK)
	                            INNER JOIN dbo.Marcas (NOLOCK) ON Marcas.Id = Cervejas.IdMarca
	                            WHERE Cervejas.Excluido = 0;";

            using (var conexao = new SqlConnection(_stringConexao))
            {
                try
                {
                    await conexao.OpenAsync();
                    var recuperarMarcas = await conexao.QueryAsync<CervejaViewModel>(sql);

                    return recuperarMarcas.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao recuperar todas as cervejas", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }

        public async Task<bool> RemoverCervejaAsync(int id)
        {
            const string sql = @"UPDATE dbo.Cervejas
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
                    throw new Exception("Falha ao remover a cerveja.", ex);
                }
                finally
                {
                    await conexao.CloseAsync();
                }
            }
        }
    }
}
