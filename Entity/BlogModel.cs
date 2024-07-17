using System.ComponentModel.DataAnnotations;
namespace BlogSite.Entity
{
    public class BlogModel : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public bool IsFeatured { get; set; }
        public Guid CategoryId { get; set; }
    }
}