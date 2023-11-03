using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieDetyra.Models
{
    public class Director
    {
        [Key]
        public int? DirectorId { get; set; }
        public string? Name { get; set; }
        public int? BirthYear { get; set; }

        [JsonIgnore]
        public ICollection<Movie>? Movies { get; set; }
    }
}
