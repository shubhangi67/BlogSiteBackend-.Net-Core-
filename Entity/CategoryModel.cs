using System.ComponentModel.DataAnnotations;
namespace BlogSite.Entity
{
    public class CategoryModel : AuditEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
    }
}