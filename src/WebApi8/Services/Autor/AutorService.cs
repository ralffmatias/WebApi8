using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Dto.Autor;
using WebApi8.Models;

namespace WebApi8.Services.Autor
{
    public class AutorService : IAutorInterface
    {

        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {

                var autor = await _context.autores.FirstOrDefaultAsync(autorBanco => autorBanco.id_autor == idAutor);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum Registro Localizado.";

                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor Localizado";

                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;

            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {

                var livro = await _context.livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.id_livro == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum Registro Localizado.";

                    return resposta;
                }

                resposta.Dados = livro.Autor;
                resposta.Mensagem = "Autor Localizado";

                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;

            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {

                var autor = new AutorModel()
                {
                    nm_autor = autorCriacaoDto.nm_autor
                };

                _context.Add(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.autores.ToListAsync();
                resposta.Mensagem = "Autor Criado Com Sucesso.";

                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;

            }

        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try
            {

                var autor = await _context.autores.FirstOrDefaultAsync(a => a.id_autor == autorEdicaoDto.id_autor);
                
                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum Autor Localizado para este Identificador";

                    return resposta;
                }


                autor.nm_autor = autorEdicaoDto.nm_autor;

                _context.Update(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.autores.ToListAsync();
                resposta.Mensagem = "processamento Executado Com Sucesso.";

                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;

            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try
            {

                var autor = await _context.autores.FirstOrDefaultAsync(a => a.id_autor == idAutor);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum Autor Localizado.";
                    resposta.Status = false;

                    return resposta;
                }

                _context.Remove(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.autores.ToListAsync();
                resposta.Mensagem = "Processamento Efetuado Com Sucesso.";

                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;

            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {

                var autores = await _context.autores.ToListAsync();

                resposta.Dados = autores;
                resposta.Mensagem = "Processamento Efetuado Com Sucesso.";

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
