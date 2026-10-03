using System.ComponentModel.DataAnnotations;

namespace BookCatalogApi.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Author { get; set; } = string.Empty;

        [Range(1000, 2100)]
        public int Year { get; set; }

        [Range(0, 1000000)]
        public decimal Price { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}