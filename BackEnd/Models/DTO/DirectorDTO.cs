using MovieDetyra.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieDetyra.Models.DTO
{
    public class DirectorDTO
    {
        [Key]
        public int? DirectorId { get; set; }
        public string? Name { get; set; }
        public int? BirthYear { get; set; }

        //[JsonIgnore]
        //public ICollection<Movie>? Movies { get; set; }
    }
}
