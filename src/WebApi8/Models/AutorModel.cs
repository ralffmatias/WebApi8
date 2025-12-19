using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi8.Models
{
    public class AutorModel
    {
        public int id_autor { get; set; }
        public string nm_autor { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<LivroModel> Livros { get; set; } = new List<LivroModel>();
    }
}
