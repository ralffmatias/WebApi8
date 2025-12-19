using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi8.Dto.Livro;
using WebApi8.Models;
using WebApi8.Services.Livro;

namespace WebApi8.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {

        private readonly ILivroInterface _livroInterface;

        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livro = await _livroInterface.ListarLivros();
            return Ok(livro);
        }

        [HttpGet("BuscarLivrosPorIdAutor/{idAutor}")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> BuscarLivrosPorIdAutor(int idAutor)
        {
            var livro = await _livroInterface.BuscarLivrosPorIdAutor(idAutor);
            return Ok(livro);
        }

        [HttpGet("BuscarLivrosPorId/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivrosPorId(int idLivro)
        {
            var livro = await _livroInterface.BuscarLivrosPorId(idLivro);
            return Ok(livro);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var livro = await _livroInterface.CriarLivro(livroCriacaoDto);
            return Ok(livro);
        }

        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var livro = await _livroInterface.EditarLivro(livroEdicaoDto);
            return Ok(livro);
        }

        [HttpDelete("ExcluirLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> ExcluirLivro(int idLivro)
        {
            var livro = await _livroInterface.ExcluirLivro(idLivro);
            return Ok(livro);
        }

    }
}
