using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PageQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip => (Page - 1) * PageSize;
    }
}
