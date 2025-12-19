namespace WebApi8.Dto.Livro
{
    public class LivroEdicaoDto
    {
        public int id_livro { get; set; } 
        public string nm_titulo { get; set; } = string.Empty;
        public int id_autor { get; set; } = 0;
    }
}
