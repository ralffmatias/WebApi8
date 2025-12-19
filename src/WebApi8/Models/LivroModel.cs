using System.Text.Json.Serialization;

namespace WebApi8.Models
{
    public class LivroModel
    {
        public int id_livro { get; set; }
        public string nm_titulo { get; set; } = string.Empty;
        public int id_autor { get; set; }
        [JsonIgnore]
        public AutorModel Autor { get; set; }
    }
}
