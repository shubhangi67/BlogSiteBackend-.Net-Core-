using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.ViewModel.User
{
    public class UserCreateViewModel
    {
        [Required]
        public string? Name { get; set; }
         [Required]
        public string? Email { get; set; }
         [Required]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}