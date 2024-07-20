using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.ViewModel.Authorization
{
    public class LoginRequestViewModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}