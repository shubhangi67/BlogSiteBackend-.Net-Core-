namespace BlogSite.ViewModel.Blog
{
    public class BlogCreateViewModel
    {
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public bool IsFeatured { get; set; }
        public Guid CategoryId { get; set; }
    }
}