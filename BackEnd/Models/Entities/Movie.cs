using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDetyra.Models.Entities
{
    public class Movie
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
