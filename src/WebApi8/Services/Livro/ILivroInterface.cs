using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();
        Task<ResponseModel<LivroModel>> BuscarLivrosPorId(int idLivro);
        Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor);
        Task<ResponseModel<LivroModel>> CriarLivro(LivroCriacaoDto livroCriacaoDto);
        Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);
    }
}