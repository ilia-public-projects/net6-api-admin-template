using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PagedResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Results { get; set; }
    }
}
