using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IKeyValueParser
    {
        T GetJsonValue<T>(IOperationContext context, List<KeyValuePairModel> settings, string key);
        string GetSettingValueOrDefault(List<KeyValuePairModel> settings, string key);
    }
}
