using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IExcelGenerator
    {
        Task<byte[]> GenerateAsync<T>(IEnumerable<T> data, string sheetName = "", bool showTimeOnDates = false) where T : class;
    }
}
