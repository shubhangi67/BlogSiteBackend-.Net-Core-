using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Dto
{
    public class SendEmailDto
    {
        public string? toEmail { get; set; }
        public string? subject { get; set; }
        public string? body { get; set; }
    }
}