using MovieDetyra.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieDetyra.Models.DTO
{
    public class MovieDTO
    {
        [Key]
        public int? MovieId { get; set; }
        public string? Title { get; set; }
        public int? ReleaseYear { get; set; }

        public int? DirectorID { get; set; }

        [ForeignKey("DirectorID")]
        public Director? Director { get; set; }
    }
}
