using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.ViewModel.Blog
{
    public class BlogUpdateViewModel
    {
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public bool IsFeatured { get; set; }
        public Guid CategoryId { get; set; }
    }
}