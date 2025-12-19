using System.Data;
using Dapper;
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly IDbConnection _connection;

        public LivroService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {

            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var sql = @" SELECT id_livro, 
                                    nm_titulo, 
                                    id_autor 
                            FROM    livros";

                var dados = (await _connection.QueryAsync<LivroModel>(sql)).ToList();

                resposta.Dados = dados;
                resposta.Mensagem = "Processamento Efetuado com Sucesso.";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }

        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
        {

            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {

                var sql = @"SELECT	id_livro,
		                            nm_titulo,
		                            id_autor
                            FROM	livros
                            WHERE	id_autor = @idAutor";

                var dados = (await _connection.QueryAsync<LivroModel>(sql, new { idAutor })).ToList();

                if (!dados.Any())
                {
                    resposta.Mensagem = $"Nenhum Registro Localizado para o id_autor {idAutor}.";

                    return resposta;
                }

                resposta.Dados = dados;
                resposta.Mensagem = "Processamento Efetuado com Sucesso";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }

        }

        public async Task<ResponseModel<LivroModel>> BuscarLivrosPorId(int idLivro)
        {
            var resposta = new ResponseModel<LivroModel>();

            try
            {
                var sql = @" SELECT id_livro,
                                    nm_titulo,
                                    id_autor
                            FROM    LIVROS
                            WHERE   id_livro = @idLivro";

                var dados = await _connection.QueryFirstOrDefaultAsync<LivroModel>(sql, new { idLivro });

                if (dados == null)
                {
                    resposta.Mensagem = $"Nenhum Registro Encontrado Para o id_livro {idLivro}.";

                    return resposta;
                }

                resposta.Dados = dados;
                resposta.Mensagem = "Processamento Efetuado com Sucesso.";

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }

        }

        public async Task<ResponseModel<LivroModel>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var resposta = new ResponseModel<LivroModel>();

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@nm_titulo", livroCriacaoDto.nm_titulo);
                parametros.Add("@id_autor", livroCriacaoDto.id_autor);

                parametros.Add("@id_livro", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@cd_retorno", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@nm_retorno", dbType: DbType.String, size: 150, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("p_inserir_livro", parametros, commandType: CommandType.StoredProcedure);

                var idLivro = parametros.Get<int?>("@id_livro") ?? 0;
                var cdRetorno = parametros.Get<int?>("@cd_retorno") ?? 0;
                var nmRetorno = parametros.Get<string>("@nm_retorno") ?? "Erro Desconhecido.";

                if (cdRetorno > 0 || idLivro == 0)
                {
                    resposta.Mensagem = nmRetorno;

                    return resposta;
                }

                var sql = @"SELECT  id_livro,
                                    nm_titulo,
                                    id_autor
                            FROM    livros
                            WHERE   id_livro = @idLivro";

                var dados = await _connection.QueryFirstOrDefaultAsync<LivroModel>(sql, new { idLivro });

                if (dados == null)
                {
                    resposta.Mensagem = "Erro ao procurar livro inserido.";

                    return resposta;
                }

                resposta.Dados = dados;
                resposta.Mensagem = nmRetorno;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }
        }

        public async Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var resposta = new ResponseModel<LivroModel>();

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@nm_titulo", livroEdicaoDto.nm_titulo);
                parametros.Add("@id_autor", livroEdicaoDto.id_autor);
                parametros.Add("@id_livro", livroEdicaoDto.id_livro);

                parametros.Add("@cd_retorno", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@nm_retorno", dbType: DbType.String, size: 150, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("p_editar_livro", parametros, commandType: CommandType.StoredProcedure);

                var idLivro = livroEdicaoDto.id_livro;
                var cdRetorno = parametros.Get<int?>("@cd_retorno") ?? 0;
                var nmRetorno = parametros.Get<string>("@nm_retorno") ?? "Erro Desconhecido.";

                if (cdRetorno > 0)
                {
                    resposta.Mensagem = nmRetorno;

                    return resposta;
                }

                var sql = @"SELECT  id_livro,
                                    nm_titulo,
                                    id_autor
                            FROM    livros
                            WHERE   id_livro = @idLivro";

                var dados = await _connection.QueryFirstOrDefaultAsync<LivroModel>(sql, new { idLivro });

                if (dados == null)
                {
                    resposta.Mensagem = "Erro ao procurar livro inserido.";

                    return resposta;
                }

                resposta.Dados = dados;
                resposta.Mensagem = nmRetorno;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }

        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            var resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id_livro", idLivro);

                parametros.Add("@cd_retorno", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@nm_retorno", dbType: DbType.String, size: 150, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("p_excluir_livro", parametros, commandType: CommandType.StoredProcedure);

                var cdRetorno = parametros.Get<int?>("@cd_retorno") ?? 0;
                var nmRetorno = parametros.Get<string>("@nm_retorno") ?? "Erro Desconhecido.";

                if (cdRetorno > 0)
                {
                    resposta.Mensagem = nmRetorno;

                    return resposta;
                }

                var sql = @"SELECT  id_livro,
                                    nm_titulo,
                                    id_autor
                            FROM    livros";

                var dados = (await _connection.QueryAsync<LivroModel>(sql)).ToList();

                if (!dados.Any())
                {
                    resposta.Mensagem = "Erro ao procurar livro inserido.";

                    return resposta;
                }

                resposta.Dados = dados;
                resposta.Mensagem = nmRetorno;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }

        }
    }
}


